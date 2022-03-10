using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCryptoCurrency", menuName = "CryptoCurrency")]
[System.Serializable]
public class Cryptos : ScriptableObject
{
    public CryptosType myCurrency;
    public float currentValue;
    public Material cryptoMatDirt;
    public Material cryptoMatStone;
    public Material cryptoMatCobel;
    public float difficultyToMine=1;
    [Range(0, 99)]
    public float chanceOfAppearance;
}
    [System.Serializable]
public enum CryptosType{
    bitcoin,
    dogeCoins,
    Eterum,
    Tether,
    none
}