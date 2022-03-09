using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillGenerator : MonoBehaviour
{
    public GameObject drillPrefab;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        var drill = SaveSystem.Instance.mines[0].drills;
        for (int i = 0; i < drill.Count; i++)
        {
            MoneyManager.instance.AddDrill(drill[i].myType);
            var myDrill = Instantiate(drillPrefab, drill[i].pos, Quaternion.identity);
            myDrill.GetComponent<DrillBehavior>().CreatDrill(drill[i]);
        }

    }

}
