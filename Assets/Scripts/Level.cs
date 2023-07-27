using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : ScriptableObject
{
    public int levelNumber;
    public int completionPer;
    public bool isCompleted;
    public List<GameObject> grassList;
}
