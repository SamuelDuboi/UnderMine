using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MacroManager : MonoBehaviour
{
    public List<MineCard> listMine;
    public List<MineurCard> listMineur;
    public float money = 0;
    public TextMeshProUGUI moneyText;

    public MineCard selectedMine;
    public MineurCard selectedMineur;

    private bool canGenerateIncome = true;

    private void Start()
    {
        StartCoroutine(GenerateIncome());
    }

    private void Update()
    {
        moneyText.text = (Mathf.Round(money*100) / 100) + " $";
    }

    private IEnumerator GenerateIncome()
    {
        while(canGenerateIncome)
        {
            money += CalculateIncomePerQuarterSecond();
            yield return new WaitForSeconds(0.25f);
        }
    }

    private float CalculateIncomePerQuarterSecond()
    {
        float totalIncomePerMinute = 0.0f;
        foreach(MineCard mc in listMine)
        {
            totalIncomePerMinute += mc.incomeParMinute;
        }
        return totalIncomePerMinute/240;
    }
}
