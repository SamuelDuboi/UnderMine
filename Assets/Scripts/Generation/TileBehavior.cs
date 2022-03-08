using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public float timeToDig;
    [SerializeField][HideInInspector] private Tile myTile;
    public MeshRenderer meshRenderer;
    public BoxCollider collider;
    private Color InitColor;
    void Start()
    {
        InitColor = new Color(137f/255f, 83f/255f, 58f/255f, 50f/255f);
    }

    public TileBehavior Digg()
    {
        if (myTile.isDigged)
        {
            Selection(0);
            Digg(1);
            gameObject.layer = 8;
            //need to be changed to the actual index of the mine
            SaveSystem.Instance.Saving(0,Minor.instance.values,myTile.indexParentChunk, new TileForSave(myTile.indexParentChunk, myTile.position, myTile.isStone));
            return null;
        }

        ChangeColor(Color.Lerp( Color.red,Color.blue, myTile.Dig(Time.deltaTime) / timeToDig));
        return this;
    }
    public float Collect()
    {
        if (myTile.isDigged)
        {
            Selection(0);
            Digg(1);
            gameObject.layer = 8;
            return myTile.cryptoType.currentValue;
        }

        ChangeColor(Color.Lerp(Color.grey, Color.green, myTile.Dig(Time.deltaTime) / timeToDig));
        return 0;
    }
    public void Select()
    {
        Selection(1);
    }
    public void Target()
    {
        Selection(1);
    }
    public TileBehavior UnSelect()
    {
        Selection(0);
        return null;
    }
    private void ChangeColor(Color color)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("ColorSelection", color);
        meshRenderer.SetPropertyBlock(propBlock);
    }

    private void Selection(int value)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("Selected", value);
        meshRenderer.SetPropertyBlock(propBlock);
    }

    private void Digg(int value)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("Empty", value);
        propBlock.SetColor("ColorSelection", InitColor);
        meshRenderer.SetPropertyBlock(propBlock);
    }
    public void ApplyCrypto(int cryptoIndex,List<Cryptos> crypto, bool isStone, bool _isDigged, int indexOfChunk,Vector2  pos)
    {
        var mycrypto = crypto[cryptoIndex];
        timeToDig += indexOfChunk/3 * Minor.instance.miningSpeed; /** TileGenerator.instance.globalMultiplicator + mycrypto.difficultyToMine*/;
        if (!myTile)
        myTile = gameObject.AddComponent<Tile>();
        if (_isDigged)
        {
            myTile.Init(0, mycrypto, pos,indexOfChunk,  isStone);
            Digg(1);
            gameObject.layer = 8;
            myTile.isDigged = true;
        }
        else
        myTile.Init(timeToDig, mycrypto, pos,indexOfChunk, isStone);
        if(!isStone)
            meshRenderer.material = mycrypto.cryptoMatDirt;
        else
            meshRenderer.material = mycrypto.cryptoMatStone;

    }
    public void ApplyCrypto(int cryptoIndex, List<Cryptos> crypto, bool isStone, int indexOfChunk, Vector2 pos)
    {
        var mycrypto = crypto[cryptoIndex];
        timeToDig += indexOfChunk / 3 * Minor.instance.miningSpeed; /** TileGenerator.instance.globalMultiplicator + mycrypto.difficultyToMine*/;
        if (!myTile)
            myTile = gameObject.AddComponent<Tile>();
            myTile.Init(0, mycrypto, pos, indexOfChunk, isStone);
        Digg(1);
            gameObject.layer = 8;
            myTile.isDigged = true;
    }
    public void ApplyCrypto(int cryptoIndex, List<Cryptos> crypto)
    {
        var mycrypto = crypto[cryptoIndex];
        timeToDig = 0; /** TileGenerator.instance.globalMultiplicator + mycrypto.difficultyToMine*/;
        if (!myTile)
            myTile = gameObject.AddComponent<Tile>();
        myTile.Init(0, mycrypto);
        Digg(1);
        gameObject.layer = 8;
        myTile.isDigged = true;
        if (!myTile.isStone)
            meshRenderer.material = mycrypto.cryptoMatDirt;
        else
            meshRenderer.material = mycrypto.cryptoMatStone;
    }

    public Tile GetTile()
    {
        return myTile;
    }
    public CryptosType GetCryptoTyp()
    {
        return myTile.cryptoType.myCurrency;
    }
}
