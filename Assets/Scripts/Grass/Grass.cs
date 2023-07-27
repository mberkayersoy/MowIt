using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Grass : MonoBehaviour
{
    [Header("Time Variables")]
    public float lastCutTime; // When was the last time it was cut.
    public float growthTime; // At what time period should grass will grow.

    [Space(5)]

    [Header("Cut Variables")]
    protected const float duration = 0.5f; // Duration of cut animation when grass is mown
    protected const float minHeight = 0.5f;
    public float maxHeight;  // This variable will decide whether to cut according to the level of the machine ??
    public bool isCut; // Todo: It will be set as cuttable or uncuttable. (canCut)


    public abstract void SetGrassSize();
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !isCut)
    //    {
    //        Vector3 targetScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
    //        transform.DOScale(targetScale, duration).SetEase(Ease.OutBack).OnComplete(() =>
    //        {
    //            isCut = true;
    //            Debug.Log("Scale animation");
    //        });
    //    }
    //}
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCut)
        {
            Vector3 targetScale = new Vector3(transform.localScale.x, minHeight, transform.localScale.z);
            transform.DOScale(targetScale, duration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                isCut = true;
                //Debug.Log("Scale animation");
            });
        }
    }
}
