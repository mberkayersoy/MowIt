using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{ 
    public static UIManager instance;
    private int money=100;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetMoney(int value)
    {
        int income = value - money;
        money=value;
        GameObject incomeText = Instantiate(Resources.Load("IncomeText") as GameObject, transform);
        incomeText.GetComponent<TextMeshProUGUI>().DOFade(0, 1f);
        incomeText.transform.DOMoveY(incomeText.transform.position.y + 50f, 1f).OnComplete(() =>
        {
            Destroy(incomeText);
            moneyText.text = money.ToString();
            moneyText.transform.DOScale(1.2f, 0.1f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                moneyText.transform.DOScale(1f, 0.1f).SetEase(Ease.InBounce);
            });
        });
        
    }
}
