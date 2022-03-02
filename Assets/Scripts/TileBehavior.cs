using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public float timeToDig;
    public int moneyValue;
    private Tile myTile;
    public MeshRenderer meshRenderer;
    public BoxCollider collider;
    void Start()
    {
        myTile = new Tile(timeToDig, moneyValue);
    }

    public TileBehavior Digg()
    {
        if (myTile.isDigged)
        {
            collider.enabled = false;
            return null;
        }

        ChangeColor(Color.blue * myTile.Dig(Time.deltaTime) / timeToDig);
        return this;
    }

    public void Select()
    {
        ChangeColor(Color.yellow);
    }
    public void Target()
    {
        ChangeColor(Color.red);
    }
    public TileBehavior UnSelect()
    {
        ChangeColor(Color.black);
        return null;
    }
    private void ChangeColor(Color color)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", color);
        meshRenderer.SetPropertyBlock(propBlock);
    }
}
