using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "Score", menuName = "Score Obj")]
public class StoreScoreObj : ScriptableObject
{
    [SerializeField] private string savePath;

    public double[] highScores = new double[3]; 
    
    public void Save()
    {
        Debug.Log("Save");
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {

        Debug.Log("load");
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
}
