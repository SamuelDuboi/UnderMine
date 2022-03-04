using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public float timeToDig;
    private Tile myTile;
    public MeshRenderer meshRenderer;
    public BoxCollider collider;
    void Start()
    {
        
    }

    public TileBehavior Digg()
    {
        if (myTile.isDigged)
        {
            ChangeColor(Color.clear);
            gameObject.layer = 8;
            return null;
        }

        ChangeColor(Color.Lerp( Color.red,Color.blue, myTile.Dig(Time.deltaTime) / timeToDig));
        return this;
    }
    public float Collect()
    {
        if (myTile.isDigged)
        {
            ChangeColor(Color.clear);
            gameObject.layer = 8;
            return myTile.cryptoType.currentValue;
        }

        ChangeColor(Color.Lerp(Color.grey, Color.green, myTile.Dig(Time.deltaTime) / timeToDig));
        return 0;
    }
    public void Select()
    {
        ChangeColor(Color.yellow);
    }
    public void Target()
    {
        ChangeColor(Color.blue);
    }
    public TileBehavior UnSelect()
    {
        if (myTile.isDigged)
            ChangeColor(Color.clear);
        else
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
    public void ApplyCrypto(Cryptos crypto, bool isStone, bool _isDigged, int strat)
    {
        timeToDig += strat * TileGenerator.instance.globalMultiplicator + crypto.difficultyToMine;
        if (_isDigged)
        {
            myTile = new Tile(0, crypto);
            ChangeColor(Color.clear);
            gameObject.layer = 8;
            myTile.isDigged = true;
        }
        else
        myTile = new Tile(timeToDig, crypto);
        if(!isStone)
            meshRenderer.material = crypto.cryptoMatDirt;
        else
            meshRenderer.material = crypto.cryptoMatStone;
    }
    public CryptosType GetCryptoTyp()
    {
        return myTile.cryptoType.myCurrency;
    }
}
