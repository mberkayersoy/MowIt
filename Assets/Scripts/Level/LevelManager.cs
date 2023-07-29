using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private List<Level> levels = new List<Level>();
    public PlayerController player;

    public void GenerateNextLevel(int level)
    {
        int nextlevel = level + 1;
        Debug.Log("Level " + nextlevel + " is opened.");
    }
}
