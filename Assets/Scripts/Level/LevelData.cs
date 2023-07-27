using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public GameObject LevelObject;
    public string levelName;
    public float completionPer;
    public float targetCompletionPercentage;
    public bool isCompleted;
    public Transform grassParent;
    public List<Grass> grassList;
    public Canvas levelCanvas;

    //public void CalculateLevelCompletionPer(LevelData level)
    //{
    //    Debug.Log("CalculateLevelCompletionPer");
    //    completionPer = 0;
    //    Debug.Log("grassList.Count: " + grassList.Count);
    //    foreach (Grass grass in grassList)
    //    {
    //        if (grass.isCut)
    //        {
    //            completionPer += 1.0f / grassList.Count;
    //            Debug.Log("grass.isCut: " + grass.isCut);
    //        }
    //    }
    //    Debug.Log("completionPer: " + completionPer);
    //    if (completionPer >= targetCompletionPercentage)
    //    {
    //        isCompleted = true;
    //        Debug.Log("Level completed");
    //        // Todo: GenerateNextLevel();
    //    }
    //}

    //public void SetLevelGrasses()
    //{
    //    //grassList = new List<Grass>();
    //    //for (int i = 0; i < grassParent.childCount; i++)
    //    //{
    //    //    grassList.Add(grassParent.GetChild(i).GetComponent<Grass>());
    //    //}
    //    Grass[] grasses = grassParent.GetComponentsInChildren<Grass>();
    //    grassList = grasses.ToList();
    //}
}