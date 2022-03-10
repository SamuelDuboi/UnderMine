using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public int stratNumber;
    public List<int> drillNumber = new List<int>();
    public float firstStratCost = 100;
    public PlayerMoney playerMoney;
    public float stratValue { get; private set; }
    public List<TextMeshProUGUI> coinValue;
    public TextMeshProUGUI globalValue;
    public TextMeshProUGUI drillCost;
    public Image dollardPanel;
    private Color initPanelColor;
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
        drillCost.text = DrillCost().ToString();
    }
    private void Start()
    {
        //change to the actual index of the Mine
        var indexOfMine = ValueManager.instance.mineIndex;
        for (int i = 0; i < SaveSystem.Instance.mines[indexOfMine].cryptos.Count; i++)
        {
            playerMoney.ChangeValue(SaveSystem.Instance.mines[indexOfMine].cryptos[i], SaveSystem.Instance.mines[indexOfMine].cryptosValue[i]);
            StartCoroutine(RevenuPerSec(SaveSystem.Instance.mines[indexOfMine].cryptos[i]));
            coinValue[i].text = playerMoney.myCryptos[i].AddRevenu(0).ToString();
            drillNumber.Add(0);
        }
        initPanelColor = dollardPanel.color;
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
        
        if (ValueManager.instance.CurrentMoney > cost)
        {
            ValueManager.instance.AddCurrentMoney(- cost);
            drillCost.text = cost.ToString();
            globalValue.text = ValueManager.instance.CurrentMoney.ToString();
            drillNumber[(int)type]++;
            
            return true;
        }
        StartCoroutine(NotEnoughtMoeny());
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
        stratNumber = SaveSystem.Instance.mines[ValueManager.instance.mineIndex].strat;
        float value = 0;
        foreach (var crypto in playerMoney.myCryptos)
        {
            value += crypto.GetRealValuePerSec();
        }
        ValueManager.instance.AddCurrentMoney( StratRevenu(stratNumber) + value);
        globalValue.text = ValueManager.instance.CurrentMoney.ToString();
        StartCoroutine(SetGlobalMoney());
    }

    public void RemoveDrill(CryptosType myType)
    {
        drillNumber[(int)myType]--;
        drillCost.text = DrillCost().ToString();
    }

    IEnumerator NotEnoughtMoeny()
    {
        for (int w = 0; w < 3; w++)
        {
            for (float i = 0; i < 50; i++)
            {
                dollardPanel.color = Color.Lerp(initPanelColor, new Color(0.9f, 0, 0, initPanelColor.a), i * 0.2f);
                yield return new WaitForSeconds(0.01f);
            }
            for (float x = 50; x > 0; x--)
            {
                dollardPanel.color = Color.Lerp(initPanelColor, new Color(0.9f, 0, 0, initPanelColor.a), x * 0.02f);
                yield return new WaitForSeconds(0.02f);
            }
        }
       
    }
}
