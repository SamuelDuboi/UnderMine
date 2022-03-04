using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public List<List<GameObject>> tiles = new List<List<GameObject>>();
    private Rect rectOfChunk;
    public int numberOfCryptoPerChunk = 182;
    public Chunk(int sizeOfChunckX,int sizeOfChunkY, GameObject tile, Vector2 posOfTile, bool isStone, float sizeOfTiles, List<Cryptos> myCrypto,int strat, ref GameObject chunk,out  GameObject chunk1)
    {

        chunk1 = chunk;
        chunk1.transform.position = posOfTile;
        for (int y = 0; y < sizeOfChunkY; y++)
        {
            List<GameObject> tilesRow = new List<GameObject>();
            for (int x = 0; x < sizeOfChunckX; x++)
            {
                var myTile = GameObject.Instantiate(tile, new Vector2(posOfTile.x + x, posOfTile.y + y) * sizeOfTiles, Quaternion.identity);
                myTile.GetComponent<TileBehavior>().ApplyCrypto(ChoseCrypto(myCrypto,strat), isStone, false,strat);
                tilesRow.Add(myTile);
                tilesRow[tilesRow.Count - 1].transform.SetParent(chunk1.transform);
            }
            tiles.Add(tilesRow);
            
        }
        List<GameObject> tilesRow2 = new List<GameObject>();
        for (int x = 0; x < sizeOfChunckX; x++)
        {
            var myTile = GameObject.Instantiate(tile, new Vector2(posOfTile.x + x, posOfTile.y ) * sizeOfTiles -(Vector2.down* sizeOfChunkY), Quaternion.identity);
            myTile.GetComponent<TileBehavior>().ApplyCrypto(myCrypto[myCrypto.Count-1], isStone, true,strat);
            tilesRow2.Add(myTile);
            tilesRow2[tilesRow2.Count - 1].transform.SetParent(chunk1.transform);
        }
        tiles.Add(tilesRow2);


        rectOfChunk = new Rect(posOfTile*sizeOfTiles,new Vector2 (sizeOfChunckX,sizeOfChunkY)*sizeOfTiles);
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
