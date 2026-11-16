using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    public bool[] isActive;
    public int[] highScores;
    public int[] stars;
}
public class GameData : MonoBehaviour
{
    public static GameData gameData;
    public SaveData saveData;


    void Awake()
    {
        if (gameData == null)
        {
            DontDestroyOnLoad(this.gameObject);
            gameData = this;
            Load();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Save()
    {
        if (this != gameData || saveData == null) return;

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
        formatter.Serialize(file, saveData);
        file.Close();
        Debug.Log("Saved");
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/player.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SaveData loadedData = formatter.Deserialize(file) as SaveData;
            file.Close();

            if (loadedData != null && loadedData.isActive != null && loadedData.isActive.Length >= saveData.isActive.Length)
            {
                saveData = loadedData;
            }
            Debug.Log("Loaded");
        }

        if (saveData != null && saveData.isActive != null && saveData.isActive.Length > 0)
        {
            saveData.isActive[0] = true;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDisable()
    {
        if (this == gameData)
        {
            Save();
        }
    }
}
