using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Grass : MonoBehaviour
{
    public float lastCutTime;
    public float duration = 0.5f;
    public bool isCut;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCut)
        {
            Vector3 targetScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            transform.DOScale(targetScale, duration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                isCut = true;
                Debug.Log("Scale animation");
            });
        }

    }
}
