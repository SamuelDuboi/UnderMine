using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DrillBehavior : MonoBehaviour
{
    Drill myDrill;
    private float revenu;
    private CryptosType myType;
    private int drillNumber;
    private int stratNumber;
    public GameObject canvas;
    public TextMeshProUGUI text;
    public Image timerImage;
    private float timeToMine;
    private float currentTime;
    public ParticleSystemRenderer mat;
    public void CreatDrill(int number,int _stratNumber,Vector2 position,direction toGO, CryptosType _myType)
    {
        Rotate(toGO);
        canvas.transform.rotation = Quaternion.identity;
        canvas.transform.localPosition = Vector3.right;
        drillNumber = number;
        stratNumber = _stratNumber;
        myType = _myType;
        myDrill = new Drill(number,stratNumber, position, toGO, myType);
        timeToMine = ValueManager.instance.currentMiner.buildingSpeed * 0.05f * (stratNumber+1);
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        mat.GetPropertyBlock(propBlock);
        switch (_myType)
        {
            case CryptosType.bitcoin:
                propBlock.SetInt("bitcoin", 1);
                break;
            case CryptosType.dogeCoins:
                propBlock.SetInt("dogeCoin", 1);
                break;
            case CryptosType.Eterum:
                propBlock.SetInt("ether", 1);
                break;
            case CryptosType.Tether:
                propBlock.SetInt("tether", 1);
                break;
            default:
                break;
        }
        mat.SetPropertyBlock(propBlock);
        canvas.SetActive(true);
        StartCoroutine(TimeBeforDrill());
    }
    IEnumerator TimeBeforDrill()
    {
        timerImage.transform.parent.gameObject.SetActive(true);
        while(currentTime< timeToMine)
        {
            yield return new WaitForFixedUpdate();
            currentTime += Time.deltaTime;
            timerImage.fillAmount = currentTime / timeToMine;
        }
        timerImage.transform.parent.gameObject.SetActive(false);
        canvas.SetActive(false);
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
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        mat.GetPropertyBlock(propBlock);
        switch (myType)
        {
            case CryptosType.bitcoin:
                propBlock.SetInt("bitcoin", 1);
                break;
            case CryptosType.dogeCoins:
                propBlock.SetInt("dogeCoin", 1);
                break;
            case CryptosType.Eterum:
                propBlock.SetInt("ether", 1);
                break;
            case CryptosType.Tether:
                propBlock.SetInt("tether", 1);
                break;
            default:
                break;
        }
        mat.SetPropertyBlock(propBlock);
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
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case direction.east:
                transform.rotation = Quaternion.Euler(90, 90, 0);
                break;
            case direction.south:
                transform.rotation = Quaternion.Euler(180, 90, 0);
                break;
            case direction.west:
                transform.rotation = Quaternion.Euler(270, 90, 0);
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
