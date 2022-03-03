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
            collider.enabled = false;
            meshRenderer.enabled = false;
            return null;
        }

        ChangeColor(Color.Lerp( Color.red,Color.blue, myTile.Dig(Time.deltaTime) / timeToDig));
        return this;
    }
    public float Collect()
    {
        if (myTile.isDigged)
        {
            collider.enabled = false;
            meshRenderer.enabled = false;
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
    public void ApplyCrypto(Cryptos crypto)
    {
        myTile = new Tile(timeToDig, crypto);
        meshRenderer.material = crypto.cryptoMat;
    }
    public CryptosType GetCryptoTyp()
    {
        return myTile.cryptoType.myCurrency;
    }
}
