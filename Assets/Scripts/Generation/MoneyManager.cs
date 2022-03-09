using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public int stratNumber;
    public int drillNumber;
    public float firstStratCost = 100;
    public PlayerMoney playerMoney;
    public float globalMoney;
    public float stratValue { get; private set; }
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void Start()
    {
        //change to the actual index of the Mine
        for (int i = 0; i < SaveSystem.Instance.mines[0].cryptos.Count; i++)
        {
            playerMoney.ChangeValue(SaveSystem.Instance.mines[0].cryptos[i], SaveSystem.Instance.mines[0].cryptosValue[i]) ;
        }
    }
    public int TryChangeStrat(int value)
    {
        if (value > stratNumber)
        {
            stratNumber = value;
            stratValue = StratCost(stratNumber);
        }
        return stratNumber;
    }
    public float StratCost(int stratNumber)
    {
        if (stratNumber == 0)
            return firstStratCost;
        return StratCost(stratNumber - 1) + StratCost(stratNumber - 1) * stratNumber * 1.1f;
    }
    public float StratRevenu(int stratNumber)
    {
        return (stratNumber+1)* firstStratCost/0.25f;
    }
    public void AddRevenuByType( CryptosType myType, int drillNumber, int stratNumber)
    {
        playerMoney.ChangeValue(myType, (drillNumber+1)*StratRevenu(stratNumber)/5f);
    }
    public List<float> MoneyValues()
    {
        List<float> values = new List<float>();
        for (int i = 0; i < playerMoney.myCryptos.Count; i++)
        {
            values.Add(playerMoney.GetNumberOwned(playerMoney.myCryptos[i].myCrypto.myCurrency));
        }
        return values;
    }

    public bool TryBuyDrill(int stratNumber)
    {
        float cost = DrillCost();
        if (globalMoney > cost)
        {
            globalMoney -= cost;
            drillNumber++;
            return true;
        }
        return false;
    }
    private float DrillCost()
    {
        return firstStratCost * 0.25f * Mathf.Pow(1.5f, drillNumber);
    }
}
