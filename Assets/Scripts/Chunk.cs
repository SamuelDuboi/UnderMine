using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public List<List<GameObject>> tiles = new List<List<GameObject>>();
    private Rect rectOfChunk;
    public Chunk(int sizeOfChunck, GameObject tile, Vector2 posOfTile,int numberOfChunk, float sizeOfTiles, ref GameObject chunk,out  GameObject chunk1)
    {
        chunk1 = chunk;
        chunk1.transform.position = posOfTile;
       
        for (int y = 0; y < sizeOfChunck; y++)
        {
            List<GameObject> tilesRow = new List<GameObject>();
            for (int x = 0; x < sizeOfChunck; x++)
            {
                tilesRow.Add(GameObject.Instantiate(tile, new Vector2(posOfTile.x +x, posOfTile.y+ y) * sizeOfTiles, Quaternion.identity));
                tilesRow[tilesRow.Count - 1].transform.SetParent(chunk1.transform);
            }
            tiles.Add(tilesRow);
            
        }
        rectOfChunk = new Rect(posOfTile*sizeOfTiles,Vector2.one*sizeOfChunck*sizeOfTiles);
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