using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Grass : MonoBehaviour, IDataSaver
{
    [Header("Time Variables")]
    protected long lastCutTime; // When was the last time it was cut. (real time)
    public float growthSpeed; // At what time period should grass will grow.
    protected bool isInitialized = false;

    [Space(5)]

    [Header("Cut Variables")]
    protected const float duration = 0.5f; // Duration of cut animation when grass is mown
    public float minHeight = 0.2f;
    public float maxHeight;  // This variable will decide whether to cut according to the level of the machine ??
    public bool isCut; // Todo: It will be set as cuttable or uncuttable. (canCut)
    public int income;
    public Level grassHouse;
    public PlayerController player;

    public abstract void SetGrassSize();

    public void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        grassHouse = GetComponentInParent<Level>();
        grassHouse.GrassList.Add(this);
    }

    protected virtual void OnEnable()
    {
        LoadData();
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !isCut)
        {
            if (transform.localScale.y > player.enginePower)
            {
                // To do: UI Feedback().
                Debug.Log("Engine power is not enough to cut the grass. Please upgrade the engine");
                return;
            }
            Vector3 targetScale = new Vector3(transform.localScale.x, minHeight, transform.localScale.z);
            transform.DOScale(targetScale, duration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                isCut = true;
            });

            // Storing the current real-world time in binary format as lastCutTime
            lastCutTime = System.DateTime.Now.ToBinary();

            player.score += income * transform.localScale.y; // Update player money.
            // To do: UIManager UpdateDisplayScore()
            grassHouse.UpdateCompletionPer();
        }

    }
    

    protected virtual void OnDisable()
    {
        SaveData();
    }

    public void SaveData()
    {
        GrassData grassData = new GrassData();
        grassData.height = transform.localScale;
        grassData.isCut = isCut;
        grassData.isInitialized = true;

        // Save the grass cut time
        grassData.lastCutTime = lastCutTime;

        string json = JsonUtility.ToJson(grassData);
        PlayerPrefs.SetString($"{gameObject.name}_Data", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString($"{gameObject.name}_Data");

        // There is no data to load when the game is first opened
        // so if the json is empty or null don't load.
        if (!string.IsNullOrEmpty(json))
        {
            GrassData grassData = JsonUtility.FromJson<GrassData>(json);

            // Load the saved data
            transform.localScale = grassData.height;
            isCut = grassData.isCut;
            isInitialized = true;
            lastCutTime = grassData.lastCutTime;
        }

        if (isCut)
        {
            long binaryTime = lastCutTime;

            // Converting the binary value back to a DateTime object
            System.DateTime lastCutDateTime = System.DateTime.FromBinary(binaryTime);

            // Calculating the time elapsed since the last cut using TimeSpan
            System.TimeSpan elapsedTime = System.DateTime.Now - lastCutDateTime;


            float growthAmount = (float)elapsedTime.TotalSeconds * growthSpeed;

            // Update the size of the grass, but do not exceed the maximum height.
            Vector3 updatedScale = transform.localScale + Vector3.up * growthAmount;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Min(updatedScale.y, maxHeight), transform.localScale.z);


            // If the height of the cut grass is smaller than the maximum height
            // and larger than the minimum height, these grasses can be cut again.
            if (transform.localScale.y == maxHeight) //& transform.localScale.y > minHeight)
            {
                isCut = false;
            }
        }
    }
}

public class GrassData
{
    public Vector3 height;
    public bool isCut;
    public bool isInitialized = false;
    public long lastCutTime;
}
