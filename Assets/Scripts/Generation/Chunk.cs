using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Chunk : MonoBehaviour
{
    public List<List<GameObject>> tiles ;
    public Rect rectOfChunk;
    public int numberOfCryptoPerChunk = 182;
    public int sizeOfChunkInX;
    public void Init(int sizeOfChunckX,int sizeOfChunkY,int indexOfChunk, GameObject tile, Vector2 posOfTile, bool isStone, float sizeOfTiles, List<Cryptos> myCrypto,int strat, ref GameObject chunk,out  GameObject chunk1)
    {
        sizeOfChunkInX = sizeOfChunckX;
        if (tiles == null)
            tiles = new List<List<GameObject>>();
        chunk1 = chunk;
        chunk1.transform.position = posOfTile;
        for (int y = 0; y < sizeOfChunkY; y++)
        {
            List<GameObject> tilesRow = new List<GameObject>();
            for (int x = 0; x < sizeOfChunckX; x++)
            {
                var myTile = GameObject.Instantiate(tile, new Vector2(posOfTile.x + x, posOfTile.y + y) * sizeOfTiles, Quaternion.identity);
                var crypto = ChoseCrypto(myCrypto, strat);
                TileBehavior myTileBehavior = myTile.GetComponent<TileBehavior>();
                myTileBehavior.ApplyCrypto(myCrypto.IndexOf(crypto), myCrypto, isStone, false, indexOfChunk, new Vector2(x,y));
                myTile.transform.SetParent(chunk1.transform);
            }
            tiles.Add(tilesRow);
            
        }
        List<GameObject> tilesRow2 = new List<GameObject>();
        for (int x = 0; x < sizeOfChunckX; x++)
        {
            var myTile = GameObject.Instantiate(tile, new Vector2(posOfTile.x + x, posOfTile.y ) * sizeOfTiles -(Vector2.down* sizeOfChunkY), Quaternion.identity);
            TileBehavior myTileBehavior = myTile.GetComponent<TileBehavior>();
            myTileBehavior.ApplyCrypto(myCrypto.Count-1, myCrypto, isStone, true,indexOfChunk, new Vector2(x, 0));
            myTile.transform.SetParent(chunk1.transform);
        }
        tiles.Add(tilesRow2);
        rectOfChunk = new Rect(posOfTile*sizeOfTiles,new Vector2 (sizeOfChunckX,sizeOfChunkY)*sizeOfTiles);
    }

    public void DisableFirstRow(List<Cryptos> myCryptos)
    {
        for (int i = 0; i < sizeOfChunkInX; i++)
        {
            tiles[tiles.Count - 1][i].GetComponent<TileBehavior>().ApplyCrypto(myCryptos.Count - 1, myCryptos);
        }
    }
    public void DestroyExistingChunk(List<TileForSave> tilesToSpawn, List<Cryptos> myCryptos)
    {
        // i know but Simon Gonand told me it was ok
        if (tiles == null)
        {
            tiles = new List<List<GameObject>>();
            int yValue = 1;
            List<GameObject> tempTile = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                tempTile.Add(transform.GetChild(i).gameObject);
                if (i == yValue * sizeOfChunkInX)
                {
                    yValue++;
                    tiles.Add(tempTile);
                    tempTile = new List<GameObject>();
                }
            }
        }
        foreach (var tile in tilesToSpawn)
        {
            tiles[(int)tile.posInStart.y][(int)tile.posInStart.x].GetComponent<TileBehavior>().ApplyCrypto(myCryptos.Count - 1, myCryptos, tile.isStone, true, tile.indexParentChunk, tile.posInStart );
        }
    }
    public void IsWithinChunk(Vector2 playerPos, out direction currentDir)
    {
        currentDir = 0;
        if (rectOfChunk.Contains(playerPos))
        {
            currentDir = direction.within;
        }
        else
        {
            if (playerPos.x < rectOfChunk.xMin)
                currentDir ^= direction.west;
            else if(playerPos.x > rectOfChunk.xMax)
                currentDir ^= direction.east;

            if (playerPos.y < rectOfChunk.yMin)
                currentDir ^= direction.south;
            else if(playerPos.y >rectOfChunk.yMax)
                currentDir ^= direction.north;
        }
    }
    private Cryptos ChoseCrypto(List<Cryptos> myCryptos, int strat)
    {
        if (numberOfCryptoPerChunk == 0)
            return myCryptos[myCryptos.Count - 2];
        strat++;
        float globalWeight=0;
        foreach (var crypto in myCryptos)
        {
            globalWeight += crypto.chanceOfAppearance * strat;
        }
        float random = Random.Range(0,globalWeight);
        float actualWeight = 0;
        for (int i = 0; i < myCryptos.Count; i++)
        {
            if (random > actualWeight && random < actualWeight + myCryptos[i].chanceOfAppearance * strat)
            {
                if(i != myCryptos.Count - 2)
                numberOfCryptoPerChunk--;
                return myCryptos[i];
            }
            actualWeight += myCryptos[i].chanceOfAppearance * strat;
        }
        return myCryptos[myCryptos.Count-2];
    }
   
}

[System.Flags]
public enum  direction :byte
{
    north =1,
    east = 2,
    south =4,
    west = 8,
    within = 16,
}
