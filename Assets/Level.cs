using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level : MonoBehaviour, IDataSaver
{
    private List<Grass> grassList = new List<Grass>();
    public List<Grass> GrassList { get => grassList; set => grassList = value; }

    public int level;
    public bool isCompleted;
    public float completionPer = 0;
    public float targetCompletionPercentage;
    [SerializeField] private Canvas levelCanvas;
    [SerializeField] private Image completionImage;

    private void OnEnable()
    {
        LoadData();
    }

    private void Start()
    {
        completionImage = levelCanvas.GetComponentInChildren<Image>();
        //set fill amount of completion image with DG.Tweening
        completionImage.DOFillAmount(completionPer, 0.5f).SetEase(Ease.InBounce);
        completionPer = 0;
        foreach (Grass grass in grassList)
        {
            if (grass.transform.localScale.y != grass.maxHeight)
            {
                completionPer += 1f / grassList.Count;
                completionImage.fillAmount = completionPer;
            }
        }
    }

    public void UpdateCompletionPer()
    {
        completionPer += 1f / grassList.Count;
        completionImage.fillAmount = completionPer;

        if (Mathf.Round(completionPer * 100) >= 100)
        {
            isCompleted = true;
            Debug.Log("House Success");
            UIManager.instance.SetPanelText("HOUSE COMPLETED!");
            LevelManager.Instance.GenerateNextLevel(level);
        }
    }
    public void SaveData()
    {
        LevelData levelData = new LevelData();
        levelData.completionPer = completionPer;
        levelData.isCompleted = isCompleted;

        string json = JsonUtility.ToJson(levelData);
        PlayerPrefs.SetString($"{gameObject.name}_Data", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString($"{gameObject.name}_Data", null);
        if (!string.IsNullOrEmpty(json))
        {
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);

            // Load the saved data
            completionPer = levelData.completionPer;
            isCompleted = levelData.isCompleted;
        }
    }

    private void OnDisable()
    {
        SaveData();
    }

}

public class LevelData 
{
    public bool isCompleted;
    public float completionPer;
}
