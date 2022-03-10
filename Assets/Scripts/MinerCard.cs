using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MinerCard")]
[System.Serializable]
public class MinerCard : ScriptableObject
{

    public Rarity rarity;
    public Biome biome;
    public float movementSpeed = 1.0f;
    public float miningSpeed = 1.0f;
    public float buildingSpeed = 30.0f;
    public float buildingCost = 15f;
    public float ethPrice = 0.001f;

    public void UpdateEthPrice()
    {
        // YOLO
        ethPrice = 0.001f + (0.02f * ((int)rarity + 1));
    }

}