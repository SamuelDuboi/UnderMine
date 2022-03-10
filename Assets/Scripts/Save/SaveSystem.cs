using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
[System.Serializable]
public class SaveSystem 
{
    public List<MineData>  mines ;
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
    private void NewSave(int index)
    {
        mines.Insert(index, new MineData());
        mines[index].tile = new List<TileForSave>();
        mines[index].cryptos = new List<CryptosType>();
        mines[index].cryptosValue = new List<float>();
        mines[index].drills = new List<Drill>();
        for (int i = 0; i < 4; i++)
        {
            mines[index].cryptos.Add( (CryptosType)i);
            mines[index].cryptosValue.Add((uint)0);
        }
        var dataPath = Path.Combine(Application.persistentDataPath, "mine" + index.ToString() + ".json");
        string json = JsonUtility.ToJson(mines[index]);
        Debug.Log(json);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);
    }
    public void Load()
    {
        mines = new List<MineData>();
        for (int i = 0; i < 4; i++)
        {
            var dataPath = Path.Combine(Application.persistentDataPath, "mine"+i.ToString()+".json");
            if (!File.Exists(dataPath))
                NewSave(i);
            string json = File.ReadAllText(dataPath);
            mines.Add(JsonUtility.FromJson<MineData>(json));
            Debug.Log(mines[i]);
        }
    }
    public void Saving(int mineIndex, List<float>cryptosValue, int strat, TileForSave _tile)
    {
        mines[mineIndex].tile.Add(_tile);
        mines[mineIndex].cryptosValue = cryptosValue;
        if(strat> mines[mineIndex].strat)
            mines[mineIndex].strat = strat;
        var dataPath = Path.Combine(Application.persistentDataPath, "mine" + mineIndex.ToString() + ".json");
        string json = JsonUtility.ToJson(mines[mineIndex]);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);
    }
    public void Saving(int mineIndex, Drill myDrill)
    {
        mines[mineIndex].drills.Add(myDrill);
        var dataPath = Path.Combine(Application.persistentDataPath, "mine" + mineIndex.ToString() + ".json");
        string json = JsonUtility.ToJson(mines[mineIndex]);
        StreamWriter sw = File.CreateText(dataPath);
        sw.Close();
        File.WriteAllText(dataPath, json);
    }
}

[System.Serializable]
public class MineData
{
    public int strat;
    public List<CryptosType> cryptos;
    public List<float> cryptosValue;
    public List<TileForSave> tile;
    public List<Drill> drills;
}