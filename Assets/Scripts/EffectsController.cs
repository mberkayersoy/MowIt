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

    [Space(5)]
    [Header("Smoke Variables")]
    [SerializeField] private ParticleSystem smokeVFX;
    [SerializeField] private float startSpeedMin;
    [SerializeField] private float startSpeedMax;
    [SerializeField] private float emissionRateMin;
    [SerializeField] private float emissionRateMax;
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

    public void SetSmokeVolume(State state)
    {
        ParticleSystem.MainModule mainModule = smokeVFX.main;
        ParticleSystem.EmissionModule emissionModule = smokeVFX.emission;
        if (state == State.Stop)
        {
            // mainModule.startSpeed = new ParticleSystem.MinMaxCurve(Random.Range(1, 2));
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(emissionRateMin);
        }
        else
        {
            //  mainModule.startSpeed = new ParticleSystem.MinMaxCurve(Random.Range(1, 6));
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(emissionRateMax);
        }
    }
}
