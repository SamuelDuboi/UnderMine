using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "MineCard")]
[System.Serializable]
public class MineCard : ScriptableObject
{

    public string seed = "";
    public Rarity rarity;
    public Biome biome;
    public int progress = 1;
    public float prospect = 0.0f;
    public float construct = 0.0f;
    public float incomeParMinute = 0.0f;
    public MineInventory inventory = new();

}

public class MineInventory
{
    public Dictionary<CryptosType, int> inventory;

    public MineInventory()
    {
        inventory = new Dictionary<CryptosType, int>();
        List<CryptosType> cryptoList = Enum.GetValues(typeof(CryptosType)).Cast<CryptosType>().ToList();
        foreach(CryptosType ct in cryptoList)
        {
            inventory.Add(ct, 0);
        }
    }
}
