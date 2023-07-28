using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class Level : ScriptableObject
{
    [SerializeField] public GameObject LevelObject;
    public int level;
    public float completionPer;
    public float targetCompletionPercentage;
    public bool isCompleted;
    [SerializeField] private Transform grassParent;
    [SerializeField] private List<Grass> grassList;
    [SerializeField] private Canvas levelCanvas;

    public void CalculateLevelCompletionPer()
    {
        Debug.Log("CalculateLevelCompletionPer");
        completionPer = 0;
        Debug.Log("grassList.Count: " + grassList.Count);
        foreach (Grass grass in grassList)
        {
            if (grass.isCut)
            {
                completionPer += 1.0f / grassList.Count;
                Debug.Log("grass.isCut: " + grass.isCut);
            }
        }
        Debug.Log("completionPer: " + completionPer);
        if (completionPer >= targetCompletionPercentage)
        {
            isCompleted = true;
            Debug.Log("Level completed");
            // Todo: GenerateNextLevel();
        }
    }

    public void SetLevelGrasses()
    {
        Grass[] grasses = grassParent.GetComponentsInChildren<Grass>();
        grassList = grasses.ToList();
    }
}
