using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    public int mineIndex { get; private set; }
    public float CurrentMoney { get; private set; }
    public float globalMoney { get; private set; }
    public int minerIndex { get; private set; }
    public float CurrentEtherum { get; private set; }

    public MinerCard currentMiner;

    public static ValueManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void NextMine(float value)
    {
        mineIndex += (int)value;
    }
    public void NextMiner(float value)
    {
        minerIndex += (int)value;
    }
    public void EndGame()
    {
        globalMoney += CurrentMoney;  
    }
    public void AddCurrentMoney(float value)
    {
        CurrentMoney += value;
    }
    public void AddGlobalMoney(float value)
    {
        globalMoney += value;
    }
    public void AddCurrentEtherum(float value)
    {
        CurrentEtherum += value;
    }

    public void SetMinerCard(MinerCard miner)
    {
        currentMiner = miner;
    }
}
