using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level Data")]
public class Level : ScriptableObject
{
    public string levelName;
    public int targetCompletionPercentage;
    public bool isCompleted;
    [SerializeField]
    private List<Grass> grassList = new List<Grass>();

    public List<Grass> GrassList { get => grassList; set => grassList = value; }

    public void CalculateLevelCompletionPer()
    {
        int completedGrassCount = grassList.Count(grass => grass.isCut);
        float completionPercentage = (float)completedGrassCount / grassList.Count;
        isCompleted = completionPercentage >= (targetCompletionPercentage / 100f);
        if (isCompleted)
        {
            // Todo: GenerateNextLevel();
            Debug.Log("Level " + levelName + " is completed!");
        }
    }
}
