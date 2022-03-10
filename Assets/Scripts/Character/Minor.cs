using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Minor : Character
{
    private Vector2 movement;
    private TileBehavior tile;
    [HideInInspector] public List<float> values;
    public static Minor instance;
    public float miningSpeed = 1;
    public float GlobalMoney =10000;
    private Rect colliderRect;
    public GameObject DrillPrefab;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }

    internal override IEnumerator Start()
    {
        yield return new WaitForFixedUpdate();
        values = MoneyManager.instance.MoneyValues();
       
        colliderRect = new Rect(playerMovement.boxCollider.center, playerMovement.boxCollider.size);
    }
    internal override  void Update()
    {
        /*if (tile)
        {
            //mettre ici le son du posage de foreuse
            //créer une condition pour que cela ne se joue qu'une fois puis cf lign 45
            float value = tile.Collect();
            if (value > 0)
            {
                //reset ici la condition pour que le son de foreuse puisse de nouveau etre jouer
                // coinValue[(int)tile.GetCryptoTyp()].text = currentPlayer.ChangeValue(tile.GetCryptoTyp(), 1).ToString();
                values[(int)tile.GetCryptoTyp()]++;
                coinValue[(int)tile.GetCryptoTyp()].text =values[(int)tile.GetCryptoTyp()].ToString();
                tile = null;
            }
        }*/
    }
    internal override void DoubleClick()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray mousRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        movement = Vector2.zero;
        tile = null;
        if (Physics.Raycast(mousRay, out hit,Mathf.Infinity, playerMovement.layerMask))
        {
            movement = hit.collider.transform.position - transform.position;
        }
        if (movement.magnitude <= 1f && movement.magnitude != 0)
        {
            tile = hit.collider.GetComponent<TileBehavior>();
            if (tile.GetCryptoTyp() == CryptosType.none)
                return;
            direction mydirection;
            if (movement.x > 0.1)
                mydirection = direction.east;
            else if (movement.x < -0.1)
                mydirection = direction.west;
            else if (movement.y > 0.1)
                mydirection = direction.north;
            else
                mydirection = direction.south;
            if (tile)
                BuyDrill(tile, mydirection);
        }
    }

    public void BuyDrill(TileBehavior targetTile,direction direction)
    {
        if (MoneyManager.instance.TryBuyDrill(targetTile.GetCryptoTyp()))
        {
            Vector2 posToSpawn = targetTile.transform.position;
            switch (direction)
            {
                case direction.north:
                    posToSpawn -= Vector2.up;
                    break;
                case direction.east:
                    posToSpawn -= Vector2.right;
                    break;
                case direction.south:
                    posToSpawn -= Vector2.down;
                    break;
                case direction.west:
                    posToSpawn -= Vector2.left;
                    break;
                default:
                    break;
            }
            var drill = Instantiate(DrillPrefab, posToSpawn, Quaternion.identity);
            drill.GetComponent<DrillBehavior>().CreatDrill(MoneyManager.instance.drillNumber[(int)targetTile.GetCryptoTyp()], targetTile.GetSratNumber(), posToSpawn, direction, targetTile.GetCryptoTyp());
        }
    }

}
