using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyGrass : Grass
{
    public override void SetGrassSize()
    {
        throw new System.NotImplementedException();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !isCut)
    //    {
    //        Vector3 targetScale = new Vector3(transform.localScale.x, minHeight, transform.localScale.z);
    //        transform.DOScale(targetScale, duration).SetEase(Ease.OutBack).OnComplete(() =>
    //        {
    //            isCut = true;
    //            Debug.Log("Scale animation");
    //        });
    //    }
    //}
}
