using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    public static EffectsController instance;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject engine;
    [SerializeField] private float shakeStrength;
    [SerializeField] private float shakeTime;
    [SerializeField] private int randomness;
    [SerializeField] private int vibrato;
    [SerializeField] private int shakeMultiplier;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        body.transform.DOShakePosition(shakeTime/shakeMultiplier, shakeStrength/shakeMultiplier, vibrato/shakeMultiplier, randomness, false, true).SetLoops(-1);
        engine.transform.DOShakePosition(shakeTime, shakeStrength, vibrato, randomness, false, true).SetLoops(-1);
    }
}
