using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelData> levels = new List<LevelData>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (LevelData item in levels)
            {
                CalculateLevelCompletionPer(item);
            }
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


    //public void SetLevelGrasses(LevelData level)
    //{
    //    Grass[] grasses = level.grassParent.GetComponentsInChildren<Grass>();
    //    Debug.Log("grasses: " + grasses.Length);
    //    level.grassList = grasses.ToList();
    //    Debug.Log("level.grassList : " + level.grassList.Count);
    //    foreach (Grass item in level.grassList)
    //    {
    //        Debug.Log(item.gameObject.name);
    //    }
    //}

    // Update is called once per frame

}
