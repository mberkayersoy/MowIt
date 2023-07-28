using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIManager : MonoBehaviour
{ 
    public static UIManager instance;
    private int money=100;
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetMoney(int value)
    {
        money=value;
        moneyText.text = money.ToString();
        moneyText.transform.DOScale(1.5f, 0.5f).SetEase(Ease.InOutBounce).OnComplete(() =>
        {
            moneyText.transform.DOScale(1f, 0.5f).SetEase(Ease.InOutBounce);
        });
    }
}
