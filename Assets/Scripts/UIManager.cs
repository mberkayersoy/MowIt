using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private int money;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI panelText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void MoneyIncrease(int income, int currentMoney)
    {
        TextMeshProUGUI incomeText = Instantiate(Resources.Load("IncomeText") as GameObject, transform).GetComponent<TextMeshProUGUI>();
        incomeText.DOFade(0, 1f);
        incomeText.text = income.ToString();
        incomeText.transform.DOMoveY(incomeText.transform.position.y + 50f, 1f).OnComplete(() =>
        {
            Destroy(incomeText.gameObject);
            UpdateMoneyText(currentMoney);
        });

    }

    public void MoneyDecrease(int income, int currentMoney)
    {
        TextMeshProUGUI incomeText = Instantiate(Resources.Load("IncomeText") as GameObject, transform).GetComponent<TextMeshProUGUI>();
        incomeText.DOFade(0, 1f);
        incomeText.color = Color.red;
        incomeText.text = income.ToString();
        incomeText.transform.DOMoveY(incomeText.transform.position.y - 50f, 1f).OnComplete(() =>
        {
            Destroy(incomeText.gameObject);
            UpdateMoneyText(currentMoney);
        });
    }
    public void UpdateMoneyText(int currentMoney)
    {
        moneyText.text = currentMoney.ToString();
        moneyText.transform.DOScale(1.2f, 0.1f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            moneyText.transform.DOScale(1f, 0.1f).SetEase(Ease.InBounce);
        });
    }

    public void SetPanelText(string text)
    {
        panelText.text = text;
        panelText.transform.DOScale(1, 0.5f).OnComplete(() =>
        {
            panelText.DOFade(0, 1f).SetDelay(1f).OnComplete(() =>
            {
                panelText.transform.localScale= Vector3.zero;
                panelText.text = "";
                panelText.color = Color.white;
            });
        });
    }
}