using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;
    [ContextMenu("To Json Data")]
    void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(playerData,true); //true는 사람이 읽기좋게만듬.
        string path = Path.Combine(Application.dataPath + "/playerData.json");
        File.WriteAllText(path, jsonData);
    }
}

[System.Serializable]
public class PlayerData
{
    public int rank;
    public float score;
    public int date;
}
