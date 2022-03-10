using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrillBehavior : MonoBehaviour
{
    Drill myDrill;
    private float revenu;
    private CryptosType myType;
    private int drillNumber;
    private int stratNumber;
    public GameObject canvas;
    public TextMeshProUGUI text;
    public void CreatDrill(int number,int _stratNumber,Vector2 position,direction toGO, CryptosType _myType)
    {
        Rotate(toGO);
        canvas.transform.rotation = Quaternion.identity;
        canvas.transform.position += Vector3.right;
        drillNumber = number;
        stratNumber = _stratNumber;
        myType = _myType;
        myDrill = new Drill(number,stratNumber, position, toGO, myType);
        SaveSystem.Instance.Saving(ValueManager.instance.mineIndex, myDrill);
        StartCoroutine(GenerateMoney());
    }

    public void CreatDrill(Drill _myDrill)
    {
        Rotate(_myDrill.direction);
        canvas.transform.rotation = Quaternion.identity;
        canvas.transform.localPosition += Vector3.right;
        drillNumber = _myDrill.number;
        stratNumber = _myDrill.stratNumber;
        myType = _myDrill.myType;
        myDrill = _myDrill;
        StartCoroutine(GenerateMoney());
    }
    IEnumerator GenerateMoney()
    {
        yield return new WaitForSeconds(1f);
        canvas.SetActive(true);
        text.text = "+ " +(0.05*(float)(stratNumber+1) ).ToString();
        text.transform.localPosition = Vector3.zero;
        text.color += new Color(0, 0, 0, 1);
        StartCoroutine(MoveText());
        StartCoroutine(GenerateMoney());
    }
    private void Rotate(direction toGO)
    {
        switch (toGO)
        {
            case direction.north:
                transform.rotation = Quaternion.Euler(270, 90, 0);
                break;
            case direction.east:
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case direction.south:
                transform.rotation = Quaternion.Euler(90, 90, 0);
                break;
            case direction.west:
                transform.rotation = Quaternion.Euler(180, 90, 0);
                break;
            default:
                break;
        }
    }
   
    IEnumerator MoveText()
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            text.transform.localPosition += Vector3.up*0.01f;
            text.color -= new Color(0, 0, 0, 0.01f);
        }
        canvas.SetActive(false);
    }
    public void TryDestroy(direction _direction)
    {
        if(_direction ==myDrill.direction)
        {
            MoneyManager.instance.RemoveDrill(myDrill.myType);
            SaveSystem.Instance.mines[ValueManager.instance.mineIndex].drills.Remove(myDrill);
            Destroy(gameObject);
        }
    }
}
