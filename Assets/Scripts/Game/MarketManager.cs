using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketManager : MonoBehaviour, IDataSaver
{
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int cost;
    public bool isUpgrading;
    [SerializeField] private Image upgradeImage;
    [SerializeField] private float upgradeTime;
    [SerializeField] private Texture updatetexture;
    float remainingTime;
    PlayerController player;
    UIManager uiManager;

    private void OnEnable()
    {
        LoadData();
    }

    private void Update()
    {
        if (isUpgrading)
        {
            remainingTime += Time.deltaTime;
            float fillAmount = remainingTime / upgradeTime;
            upgradeImage.fillAmount = fillAmount;

            if (remainingTime >= upgradeTime)
            {
                StartCoroutine(StopUpgradeProcess());
            }
        }
        else
        {
            upgradeImage.fillAmount = 0;
            remainingTime = 0;
        }
    }

    private void Start()
    {
        uiManager = UIManager.instance;
        costText.text = cost.ToString();
        remainingTime = 0;
    }

    void UpdateCost()
    {
        cost *= 2;
        costText.text = cost.ToString();
    }
    IEnumerator StopUpgradeProcess()
    {
        player.SetMoney(-cost);
        UpdateCost();
        player.EnginePower++;
        uiManager.SetPanelText("Engine upgraded");
        upgradeImage.fillAmount = 0;
        remainingTime = 0;

        yield return new WaitForSeconds(1f);
        if (player.GetMoney() < cost)
        {
            isUpgrading = false;
            uiManager.SetPanelText("You don't have enoguh money to upgrade engine");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();

            if (player.GetMoney() >= cost)
            {
                isUpgrading = true;
            }
            else
            {
                uiManager.SetPanelText("You don't have enough money to upgrade engine!");
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isUpgrading = false;
        }
    }

    public void SaveData()
    {
        MarketData marketData = new MarketData();
        marketData.currentCost = cost;
        string json = JsonUtility.ToJson(marketData);
        PlayerPrefs.SetString($"{gameObject.name}_Data", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString($"{gameObject.name}_Data", null);

        if (!string.IsNullOrEmpty(json))
        {
            MarketData marketData = JsonUtility.FromJson<MarketData>(json);

            cost = marketData.currentCost;

        }
    }

    private void OnDisable()
    {
        SaveData();
    }
}

public class MarketData
{
    public int currentCost;
}
