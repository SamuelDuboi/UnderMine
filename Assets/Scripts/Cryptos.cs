using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCryptoCurrency", menuName = "CryptoCurrency")]
[System.Serializable]
public class Cryptos : ScriptableObject
{
    public CryptosType myCurrency;
    public float currentValue;
    public Material cryptoMat;
    [Range(0, 99)]
    public float chanceOfAppearance;
}
    [System.Serializable]
public enum CryptosType{
    bitcoin,
    dogeCoins,
    Eterum,
    none
}