using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class FakeStockMarket : MonoBehaviour
{
    public static FakeStockMarket instance;

    public Dictionary<CryptosType, float> tradeValues;

    private List<CryptosType> cryptoList = Enum.GetValues(typeof(CryptosType)).Cast<CryptosType>().ToList();

    public void Start() 
    {
        if(instance == null)
        {
            instance = this;


            tradeValues = new Dictionary<CryptosType, float>()
            {
                {CryptosType.bitcoin, 1500.0f},
                {CryptosType.dogeCoins, 50.0f},
                {CryptosType.Eterum, 800.0f}
            };

            foreach(CryptosType ct in cryptoList)
            {
                tradeValues[ct] = tradeValues[ct] + UnityEngine.Random.Range(-tradeValues[ct]*0.35f, tradeValues[ct]*0.35f);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    public void UpdateValues()
    {
        // J'aurais jamais le time
    }
}

public enum StockState
{
    STABLE,
    SLOW_RISE,
    SLOW_FALL,
    FAST_RISE,
    FAST_FALL,
    CHAOTIC
}