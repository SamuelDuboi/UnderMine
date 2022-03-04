using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Minor : Character
{
    private Vector2 movement;
    private TileBehavior tile;
    public List<TextMeshProUGUI>coinValue = new List<TextMeshProUGUI>();
    private List<int> values;
    internal override void Start()
    {
        base.Start();
        /*  for (int i = 0; i < currentPlayer.myCryptos.Count; i++)
          {
              coinValue[i].text = currentPlayer.GetNumberOwned(i).ToString();
          }*/
        values = new List<int>();
        for (int i = 0; i <3; i++)
        {
            coinValue[i].text = (3*i).ToString();
            values.Add(3 * 1);
        }
    }
    internal override  void Update()
    {
        if (tile)
        {
            float value = tile.Collect();
            if (value > 0)
            {
                // coinValue[(int)tile.GetCryptoTyp()].text = currentPlayer.ChangeValue(tile.GetCryptoTyp(), 1).ToString();
                values[(int)tile.GetCryptoTyp()]++;
                coinValue[(int)tile.GetCryptoTyp()].text =values[(int)tile.GetCryptoTyp()].ToString();
                tile = null;
            }
        }
    }
    internal override void DoubleClick()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray mousRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        movement = Vector2.zero;
        tile = null;
        if (Physics.Raycast(mousRay, out hit))
        {
            movement = hit.collider.transform.position - transform.position;
        }
        if (movement.magnitude < 1.7f)
        {
            tile = hit.collider.GetComponent<TileBehavior>();
        }
    }
}
