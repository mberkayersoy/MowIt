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

public class PlayerController : MonoBehaviour, IDataSaver
{
    private Rigidbody rb;

    public int queueSize = 3;   
    private Queue<State> directions;
    public State currentState;
    public float smoothTime = 0.250f; // speed
    public Vector3 targetPosition;
    Vector3 velocity = Vector3.zero;

    // Input
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;

    // Persistent Datas
    public float enginePower = 3;
    public Vector3 lastCheckPoint;
    public int score;
    public bool isInitialized;

    void Start()
    {
        directions = new Queue<State>(queueSize);
        rb = GetComponent<Rigidbody>();
        currentState = State.Stop;
    }
    private void OnEnable()
    {
        ParticleSystem vfx;
        LoadData();
    }

    void Update()
    {
        //GetInputs();
        TouchInputs();
        RotatePlayer();
        Move();
    }

    
    void TouchInputs()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position; // First Touch Position
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPosition = touch.position; // Last Touch Position

                // Direction Vector
                Vector2 touchVector = touchEndPosition - touchStartPosition;


                float angle = Mathf.Atan2(touchVector.y, touchVector.x) * Mathf.Rad2Deg;

                if (angle < 45f && angle > -45f)
                {
                    SetQueue(State.Right);
                }
                else if (angle > 45f && angle < 135f)
                {
                    SetQueue(State.Forward);
                }
                else if (angle > 135f || angle < -135f)
                {
                    SetQueue(State.Left);
                }
                else
                {
                    SetQueue(State.Backward);
                }
            }
        }
    }

    void Move()
    {
        if((transform.position - targetPosition).magnitude > 0.01f && currentState != State.Stop)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            //To do: transform.DOMove(); 
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
            Debug.Log(obj.name);

            if (obj.CompareTag("rock"))
            {
                targetPosition = hit.point - transform.forward / 2;
            }
            else if (obj.CompareTag("CheckPoint"))
            {
                targetPosition = hit.point + transform.forward / 2;
                lastCheckPoint = targetPosition;
            }
        }
    }
    // create raycast gizmo to see where the raycast is going
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPosition);
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

    void GetInputs()
    {
        if (directions.Count >= queueSize) return;

        if (Input.GetKeyDown(KeyCode.W))
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
    }

    public void SaveData()
    {
        PlayerData playerData = new PlayerData();
        playerData.score = score;
        playerData.lastCheckPoint = lastCheckPoint;
        playerData.initilaized = isInitialized;
        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString($"{gameObject.name}_Data", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        string json = PlayerPrefs.GetString($"{gameObject.name}_Data", null);

        if (!string.IsNullOrEmpty(json))
        {
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            // Load the saved data
            transform.position = playerData.lastCheckPoint;
            score = playerData.score;
            UIManager.instance.SetMoney(score);
        }
        else
        {
            lastCheckPoint = transform.position;
           // transform.position = new Vector3(0, 0.5f, 0);
        }
    }

    private void OnDisable()
    {
        SaveData();
    }

}

public class PlayerData
{
    public Vector3 lastCheckPoint;
    public int score;
    public bool initilaized;
    public float enginePower;
}




