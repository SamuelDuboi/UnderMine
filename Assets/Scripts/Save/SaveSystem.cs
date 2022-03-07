using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
[System.Serializable]
public class SaveSystem 
{
   public TempClas tempClass;
    private static SaveSystem instance = null;
    public static SaveSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveSystem();
            }
            return instance;
        }
    }
   
    public SaveSystem()
    {
        Load();
    }
    private void NewSave()
    {
        tempClass = new TempClas();
        tempClass.tile = new List<TileForSave>();
        var dataPath = Path.Combine(Application.persistentDataPath, "save.json");
        string json = JsonUtility.ToJson(tempClass);
        Debug.Log(json);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);
    }
    public void Load()
    {
        var dataPath = Path.Combine(Application.persistentDataPath, "save.json");
        if (!File.Exists(dataPath))
            NewSave();
        string json = File.ReadAllText(dataPath);
        tempClass = JsonUtility.FromJson<TempClas>(json);
        Debug.Log(tempClass);
    }
    public void Saving(TileForSave _tile)
    {
        tempClass.tile.Add(_tile);
        var dataPath = Path.Combine(Application.persistentDataPath, "save.json");
        string json = JsonUtility.ToJson(tempClass);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);
    }
}

[System.Serializable]
public class TempClas
{
        public List<TileForSave> tile;
}