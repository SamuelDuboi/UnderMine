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
    internal override void Start()
    {
        base.Start();
        for (int i = 0; i < currentPlayer.myCryptos.Count; i++)
        {
            coinValue[i].text = currentPlayer.GetNumberOwned(i).ToString();
        }
    }
    internal override  void Update()
    {
        if (tile)
        {
            float value = tile.Collect();
            if (value > 0)
            {
                coinValue[(int)tile.GetCryptoTyp()].text = currentPlayer.ChangeValue(tile.GetCryptoTyp(), value).ToString();
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
