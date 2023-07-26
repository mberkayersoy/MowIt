using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Forward,
    Backward,
    Right,
    Left,
    Stop
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private Rigidbody rb;

    public int queueSize = 2;
    [SerializeField] public Queue<State> directions;
    public State currentState;
    public Vector3 targetPosition;
    public float smoothTime = 0.250f;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        directions = new Queue<State>(queueSize);
        rb = GetComponent<Rigidbody>();
        currentState = State.Stop;
    }

    void Update()
    {
        GetInputs();
        RotatePlayer();
        Move();
    }

    void Move()
    {
        if((transform.position-targetPosition).magnitude>0.1f && currentState != State.Stop)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            currentState = State.Stop;
        }

    }

    void FindTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward , out hit, Mathf.Infinity))
        {
            GameObject obj = hit.collider.gameObject;
            Debug.Log(obj.tag);

            if (obj.CompareTag("rock"))
            {
                targetPosition = hit.point - transform.forward;
            }
        }
    }
    //create raycast gizmo to see where the raycast is going
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 1000);
    }

    void GetInputs()
    {
        if (directions.Count >= queueSize) return;

        if(Input.GetKeyDown(KeyCode.W))
        {
            directions.Enqueue(State.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            directions.Enqueue(State.Backward);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            directions.Enqueue(State.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            directions.Enqueue(State.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            directions.Enqueue(State.Stop);
        }
    }
    void RotatePlayer()
    {
        if (currentState == State.Stop)
        {
            if(directions.Count <= 0)
            {
                return;
            }

            currentState = directions.Dequeue();
            Quaternion angle;

            switch (currentState)
            {
                case State.Forward:
                    angle = Quaternion.Euler(0f, 0f, 0f);
                    transform.rotation = angle;
                    break;
                case State.Backward:
                    angle = Quaternion.Euler(0f, 180f, 0f);
                    transform.rotation = angle;
                    break;
                case State.Right:
                    angle = Quaternion.Euler(0f, 90f, 0f);
                    transform.rotation = angle;
                    break;
                case State.Left:
                    angle = Quaternion.Euler(0f, 270f, 0f);
                    transform.rotation = angle;
                    break;

                default:
                    break;
            }
            FindTarget();
        }     
    }
}
