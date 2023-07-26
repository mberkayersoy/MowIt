using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float acceleration = 10f;
    public float maxSpeed;
    private Rigidbody rb;
    private bool isRight, isLeft, isForward, isBack, canMove;
    public float rayDistance=0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRotation();
        Vector3 rayDirection = -transform.right;
        RaycastHit hit;
        canMove=true;
        if (Physics.Raycast(transform.position, rayDirection, out hit, rayDistance))
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log(obj.tag);
            if (obj.CompareTag("rock"))
            {
                moveSpeed = 0;
                isRight = false;
                isForward = false;
                isLeft = false;
                isBack = false;
                canMove=true;
                Vector3 correction = new Vector3(
                    Mathf.Round(transform.position.x),
                    transform.position.y,
                    Mathf.Round(transform.position.z));
                transform.position = correction;
            }
            else
            {
                canMove=true;
                 if (moveSpeed<maxSpeed)
                 { 
                     moveSpeed += acceleration*Time.deltaTime;
                 }
            }
        }
    }
    void ChangeRotation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            isForward = true;
            Quaternion angle = Quaternion.Euler(0f, 90, 0f); 
            transform.rotation = angle;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
            Quaternion angle = Quaternion.Euler(0f, 0f, 0f); 
            transform.rotation = angle;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            isBack = true;
            Quaternion angle = Quaternion.Euler(0f, 270f, 0f); 
            transform.rotation = angle;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isRight = true;
            Quaternion angle = Quaternion.Euler(0f, 180f, 0f); 
            transform.rotation = angle;
        }
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            float moveHorizontal = 0;
            float moveVertical = 0;
            if (isForward)
                moveVertical = 1;
            else if (isBack)
            {
                moveVertical = -1;
            }
            else if(isRight)
            {
                moveHorizontal = 1;
            }
            else if(isLeft)
            {
                moveHorizontal = -1;
            }
            else
            {
                moveVertical = 0;
                moveHorizontal = 0;
            }
            Vector3 hareket = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
            transform.Translate(hareket,Space.World);
        }
    }
}
