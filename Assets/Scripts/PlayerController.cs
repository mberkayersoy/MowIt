using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    public int queueSize = 3;   
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
        //Debug.Log("directions.Count. " + directions.Count);
    }

    void Move()
    {
        if((transform.position - targetPosition).magnitude > 0.01f && currentState != State.Stop)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            currentState = State.Stop;
            Vector3 correction = new Vector3(
                    Mathf.Round(transform.position.x),
                    transform.position.y,
                    Mathf.Round(transform.position.z));
            transform.position = correction;
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
                targetPosition = hit.point - transform.forward / 2;
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
            SetQueue(State.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetQueue(State.Backward);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SetQueue(State.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetQueue(State.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SetQueue(State.Stop);
        }
    }

    void SetSpeed()
    {
        switch (directions.Count)
        {
            case 0:
                smoothTime = 0.18f;
                break;
            case 1:
                smoothTime = 0.14f;
                break;
            case 2:
                smoothTime = 0.10f;
                break;
            default:
                break;
        }
    }

    void SetQueue(State state)
    {
        if (directions.TryPeek(out State firstState))
        {
            if (firstState != state)
            {
                directions.Enqueue(state);
            }
        }
        else
        {
            directions.Enqueue(state);
        }

    }
    void RotatePlayer()
    {
        if (currentState == State.Stop)
        {

            if (directions.Count <= 0)
            {
                return;
            }
            currentState = directions.Dequeue();
            SetSpeed();
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

    void HitAnimation()
    {
        transform.DOScaleZ(transform.localScale.z + 0.1f, 0.1f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOScaleZ(transform.localScale.z, 0.1f)
                    .SetEase(Ease.InBack);
            });
    }
}
