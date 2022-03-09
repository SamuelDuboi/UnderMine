using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public int stratNumber;
    public List<int> drillNumber = new List<int>();
    public float firstStratCost = 100;
    public PlayerMoney playerMoney;
    public float globalMoney;
    public float stratValue { get; private set; }
    public List<TextMeshProUGUI> coinValue;
    public TextMeshProUGUI globalValuePerSec;
    public TextMeshProUGUI globalValue;
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    public void AddDrill(CryptosType type)
    {
        drillNumber[(int)type]++;
    }
    private void Start()
    {
        //change to the actual index of the Mine
        for (int i = 0; i < SaveSystem.Instance.mines[0].cryptos.Count; i++)
        {
            playerMoney.ChangeValue(SaveSystem.Instance.mines[0].cryptos[i], SaveSystem.Instance.mines[0].cryptosValue[i]);
            StartCoroutine(RevenuPerSec(SaveSystem.Instance.mines[0].cryptos[i]));
            coinValue[i].text = playerMoney.myCryptos[i].AddRevenu(0).ToString();
            drillNumber.Add(0);
        }
        StartCoroutine(SetGlobalMoney());
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
        return (stratNumber+1)* firstStratCost*0.0025f;
    }
    IEnumerator RevenuPerSec(CryptosType myType)
    {
        yield return new WaitForSeconds(1f);
        var value = (drillNumber[(int)myType] + 1) * StratRevenu(stratNumber) / 5f;
        playerMoney.ChangeValue(myType, value);
        for (int i = 0; i < playerMoney.myCryptos.Count; i++)
        {
            coinValue[i].text = playerMoney.myCryptos[i].AddRevenu(0).ToString();
        }
        StartCoroutine(RevenuPerSec(myType));
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

    public bool TryBuyDrill(CryptosType type)
    {
        float cost = DrillCost();
        if (globalMoney > cost)
        {
            globalMoney -= cost;
            drillNumber[(int)type]++;
            
            return true;
        }
        return false;
    }
    private float DrillCost()
    {
        int globalDrillNumber = 0;
        foreach (var number in drillNumber)
        {
            globalDrillNumber += number;
        }


        return firstStratCost * 0.25f * Mathf.Pow(1.5f, globalDrillNumber);
    }

    IEnumerator SetGlobalMoney()
    {
        yield return new WaitForSeconds(1f);
        stratNumber = SaveSystem.Instance.mines[0].strat;
        float value = 0;
        foreach (var crypto in playerMoney.myCryptos)
        {
            value += crypto.GetRealValuePerSec();
        }
        globalValuePerSec.text = (StratRevenu(stratNumber) + value).ToString() + "/s";
        globalMoney += StratRevenu(stratNumber) + value;
        globalValue.text = globalMoney.ToString();
        StartCoroutine(SetGlobalMoney());
    }

    public void RemoveDrill(CryptosType myType)
    {
        drillNumber[(int)myType]--;
    }
}
