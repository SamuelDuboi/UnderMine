using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MineurCard")]
[System.Serializable]
public class MineurCard : ScriptableObject
{

    public MineurJob job;
    public Rarity rarity;
    public float movementSpeed = 1.0f;
    public float precision = 0.0f;
    public float miningSpeed = 1.0f;

}

public enum MineurJob
{
    INGENIEUR,
    PROSPECTEUR,
    MINEUR
}