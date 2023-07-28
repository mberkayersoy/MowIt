using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<LevelData> levels = new List<LevelData>();
    [SerializeField] private List<Level> levelscriptable = new List<Level>();
    public PlayerController player;

    private void Start()
    {
        foreach (var item in levels)
        {
            SetLevelGrasses(item);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateLevelCompletionPer(levels[0]);
            Debug.Log(levels[0].grassList.Count);
        }
    }

    public void CalculateLevelCompletionPer(LevelData level)
    {
        level.completionPer = 0;
        foreach (Grass grass in level.grassList)
        {
            if (grass.isCut)
            {
                level.completionPer += 1.0f / level.grassList.Count;
            }
        }
        Debug.Log("completionPer: " + level.completionPer);
        if (level.completionPer >= level.targetCompletionPercentage)
        {
            level.isCompleted = true;
            Debug.Log("Level completed");
            // Todo: GenerateNextLevel();
        }
    }

    public void SetLevelGrasses(LevelData level)
    {
        Grass[] grasses = level.grassParent.GetComponentsInChildren<Grass>();
        Debug.Log("grasses: " + grasses.Length);
        level.grassList = grasses.ToList();

        Debug.Log("level.grassList : " + level.grassList.Count);

        foreach (Grass item in level.grassList)
        {
            Debug.Log(item.gameObject.name);
        }
    }

}
