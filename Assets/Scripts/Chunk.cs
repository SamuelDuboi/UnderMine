using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public List<List<GameObject>> tiles = new List<List<GameObject>>();
    private Rect rectOfChunk;
    public Chunk(int sizeOfChunckX,int sizeOfChunkY, GameObject tile, Vector2 posOfTile, bool isStone, float sizeOfTiles, List<Cryptos> myCrypto, ref GameObject chunk,out  GameObject chunk1)
    {
        chunk1 = chunk;
        chunk1.transform.position = posOfTile;
       
        for (int y = 0; y < sizeOfChunkY; y++)
        {
            List<GameObject> tilesRow = new List<GameObject>();
            for (int x = 0; x < sizeOfChunckX; x++)
            {
                var myTile = GameObject.Instantiate(tile, new Vector2(posOfTile.x + x, posOfTile.y + y) * sizeOfTiles, Quaternion.identity);
                myTile.GetComponent<TileBehavior>().ApplyCrypto(ChoseCrypto(myCrypto), isStone, false);
                tilesRow.Add(myTile);
                tilesRow[tilesRow.Count - 1].transform.SetParent(chunk1.transform);
            }
            tiles.Add(tilesRow);
            
        }
        List<GameObject> tilesRow2 = new List<GameObject>();
        for (int x = 0; x < sizeOfChunckX; x++)
        {
            var myTile = GameObject.Instantiate(tile, new Vector2(posOfTile.x + x, posOfTile.y ) * sizeOfTiles -(Vector2.down* sizeOfChunkY), Quaternion.identity);
            myTile.GetComponent<TileBehavior>().ApplyCrypto(myCrypto[myCrypto.Count-1], isStone, true);
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
    private Cryptos ChoseCrypto(List<Cryptos> myCryptos)
    {
        List<Cryptos> _cryptos = new List<Cryptos>();
        for (int i = 0; i < myCryptos.Count; i++)
        {
            float randomValue = Random.Range(0, 100);
            if (randomValue < myCryptos[i].chanceOfAppearance)
            {
                _cryptos.Add(myCryptos[i]);
            }
        }
        return _cryptos[Random.Range(0, _cryptos.Count)];
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
