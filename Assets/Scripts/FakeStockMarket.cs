using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class FakeStockMarket : MonoBehaviour
{
    public Dictionary<CryptosType, float> tradeValues;
    public Dictionary<CryptosType, StockState> tradeStates;
    public Dictionary<CryptosType, float> tradeDeltas;


    public FakeStockMarket() 
    {
        tradeValues = new Dictionary<CryptosType, float>();
        tradeStates = new Dictionary<CryptosType, StockState>();
        tradeDeltas = new Dictionary<CryptosType, float>();

        List<CryptosType> cryptoList = Enum.GetValues(typeof(CryptosType)).Cast<CryptosType>().ToList();
        foreach(CryptosType ct in cryptoList)
        {
           //TODO
        }
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