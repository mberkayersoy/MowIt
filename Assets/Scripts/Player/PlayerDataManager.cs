using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    //public Vector3 lastCheckPoint;
    //public float score;
    //// public int enginePower;
    //private void OnEnable()
    //{
    //    LoadData();
    //}
    //private void OnDisable()
    //{
    //    SaveData();
    //}

    //void SaveData()
    //{
    //    PlayerData playerData = new PlayerData();
    //    playerData.score = score;
    //    playerData.lastCheckPoint = transform.position;

    //    string json = JsonUtility.ToJson(playerData);
    //    PlayerPrefs.SetString($"{gameObject.name}_Data", json);
    //    PlayerPrefs.Save();
    //}

    //void LoadData()
    //{
    //    string json = PlayerPrefs.GetString($"{gameObject.name}_Data", null);
    //    if (json != null)
    //    {
    //        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
    //        // Load the saved data
    //        transform.position = playerData.lastCheckPoint;
    //        score = playerData.score;
    //    }
    //}

}

////[System.Serializable]
////public class PlayerData
////{
////    public Vector3 lastCheckPoint;
////    public float score;
////    public int enginePower;
////    public int upgrade; // What type of value should we use?
////}
