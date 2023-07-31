using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SwipeScript : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Image>().DOFade(0f, 1.2f).SetLoops(-1, LoopType.Yoyo);
    }
}