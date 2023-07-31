using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 20, -15);
    [SerializeField] private float smoothTime;
    Vector3 targetPosition;
    private Vector3 _currentVelocity = Vector3.zero;
    void Awake()
    {
        transform.position = target.position + offset;
    }
    private void LateUpdate()
    {
        targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}