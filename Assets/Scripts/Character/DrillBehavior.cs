using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBehavior : MonoBehaviour
{
    Drill myDrill;
    private float revenu;
    private CryptosType myType;
    private int drillNumber;
    private int stratNumber;
    public void CreatDrill(int number,int _stratNumber,Vector2 position,direction toGO, CryptosType _myType)
    {
        drillNumber = number;
        stratNumber = _stratNumber;
        myType = _myType;
        myDrill = new Drill(number,stratNumber, position, toGO, myType);
        //change index to mine index
        SaveSystem.Instance.Saving(0, myDrill);
        StartCoroutine(GenerateMoney());
    }

    public void CreatDrill(Drill _myDrill)
    {
        drillNumber = _myDrill.number;
        stratNumber = _myDrill.stratNumber;
        myType = _myDrill.myType;
        myDrill = _myDrill;
        StartCoroutine(GenerateMoney());
    }
    IEnumerator GenerateMoney()
    {
        yield return new WaitForSeconds(1f);
        MoneyManager.instance.AddRevenuByType(myType, drillNumber,stratNumber);
        StartCoroutine(GenerateMoney());
    }
   
}
