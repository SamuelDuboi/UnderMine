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
    public LayerMask drillMask;

    void Start()
    {
        InitColor = new Color(137f/255f, 83f/255f, 58f/255f, 50f/255f);
        if (myTile.DiggingState != 0)
            myTile.DiggingState /= ValueManager.instance.currentMiner.miningSpeed;
    }

    public int GetSratNumber()
    {
        return myTile.indexParentChunk / 3;
    }
    public TileBehavior Digg()
    {
        if (myTile.isDigged)
        {
            Selection(0);
            Digg(1);
            Crack(1);
            gameObject.layer = 8;
            TryDestroyDrill();
            SaveSystem.Instance.Saving(ValueManager.instance.mineIndex, MoneyManager.instance.MoneyValues(),myTile.indexParentChunk/3, new TileForSave(myTile.indexParentChunk, myTile.position, myTile.materialValue));
            return null;
        }

        Crack(myTile.Dig(Time.deltaTime) / timeToDig);
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

        Crack( myTile.Dig(Time.deltaTime) / timeToDig);
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
    private void Crack(float lerpValue)
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("CracksSpreading", (1-lerpValue)*20);
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
    public void ApplyCrypto(int cryptoIndex,List<Cryptos> crypto, int materialValue, bool _isDigged, int indexOfChunk,Vector2  pos)
    {
        var mycrypto = crypto[cryptoIndex];
        timeToDig += indexOfChunk/3 ; /** TileGenerator.instance.globalMultiplicator + mycrypto.difficultyToMine*/;
        if (!myTile)
        myTile = gameObject.AddComponent<Tile>();
        if (_isDigged)
        {
            myTile.Init(0, mycrypto, pos,indexOfChunk, materialValue);
            Digg(1);
            gameObject.layer = 8;
            myTile.isDigged = true;
        }
        else
        myTile.Init(timeToDig, mycrypto, pos,indexOfChunk, materialValue);
        if(materialValue ==0)
            meshRenderer.material = mycrypto.cryptoMatDirt;
        else if(materialValue ==1)
            meshRenderer.material = mycrypto.cryptoMatStone;
        else
            meshRenderer.material = mycrypto.cryptoMatCobel;
    }
    public void ApplyCrypto(int cryptoIndex, List<Cryptos> crypto, int materialValue, int indexOfChunk, Vector2 pos)
    {
        var mycrypto = crypto[cryptoIndex];
        timeToDig += indexOfChunk / 3 ; /** TileGenerator.instance.globalMultiplicator + mycrypto.difficultyToMine*/;
        if (!myTile)
            myTile = gameObject.AddComponent<Tile>();
            myTile.Init(0, mycrypto, pos, indexOfChunk, materialValue);
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
        if (myTile.materialValue == 0)
            meshRenderer.material = mycrypto.cryptoMatDirt;
        else if (myTile.materialValue == 1)
            meshRenderer.material = mycrypto.cryptoMatStone;
        else
            meshRenderer.material = mycrypto.cryptoMatCobel;
    }
    public void TryDestroyDrill()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector2.up, out hit, 1.5f, drillMask))
            hit.collider.GetComponent<DrillBehavior>().TryDestroy(direction.south);
        Debug.DrawRay(transform.position, Vector2.up, Color.green, 10f);
        if (Physics.Raycast(transform.position, Vector2.right, out hit, 1, drillMask))
            hit.collider.GetComponent<DrillBehavior>().TryDestroy(direction.west);
        if (Physics.Raycast(transform.position, Vector2.down, out hit, 1, drillMask))
            hit.collider.GetComponent<DrillBehavior>().TryDestroy(direction.north);
        if (Physics.Raycast(transform.position, Vector2.left, out hit, 1, drillMask))
            hit.collider.GetComponent<DrillBehavior>().TryDestroy(direction.east);
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
