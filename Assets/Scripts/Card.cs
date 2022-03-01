using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int numberOfChunkToLoad {get=> 3;}
    public int sizeOfChunk { get => 8; }
    public List<List< Chunk>> chunks=  new List<List< Chunk>>();
    public List<List< GameObject>> chunksGO=  new List<List<GameObject>>();
    public void Populate(bool vertical, Vector2 position, GameObject tilePrefab, float direction )
    {
        if (vertical)
        {
            List<Chunk> chunkRow = new List<Chunk>();
            List<GameObject> chunkRowGO = new List<GameObject>();
            if (direction!= 0)
            {
                DisableRow((int)position.y - 1);
                if (position.y + 2 < chunksGO.Count)
                {
                    EnableRow((int)position.y +1 );
                    return;
                }
                for (int i = 0; i < chunksGO[0].Count; i++)
                {
                    GameObject newGO = new GameObject();
                    chunkRow.Add(new Chunk(sizeOfChunk, tilePrefab, new Vector2(chunksGO[0][i].transform.position.x, -chunksGO.Count * sizeOfChunk) , chunksGO.Count, ref newGO, out newGO));

                    chunkRowGO.Add(newGO);
                }
                chunksGO.Add(chunkRowGO);
                chunks.Add(chunkRow);
            }
           
        }
        else
        {
            if (direction > 0)
            {
                DisableColumn((int)position.x - 1);
                if (position.y + 2 < chunksGO.Count)
                {
                    EnableColumn((int)position.x + 1);
                    return;
                }
                if (position.x + 2 < chunksGO[0].Count)
                    return;
                int xValue = chunksGO[0].Count;
                for (int i = 0; i < chunksGO.Count; i++)
                {
                    GameObject newGO = new GameObject();
                    chunks[i].Add(new Chunk(sizeOfChunk, tilePrefab, new Vector2(chunksGO[i][xValue-1].transform.position.x + 1 * sizeOfChunk, -i * sizeOfChunk) , chunksGO.Count, ref newGO, out newGO));
                    chunksGO[i].Add(newGO);
                }
            }
            else
            {
                DisableColumn(chunksGO.Count - 1);
                if (position.y + 2 < chunksGO.Count)
                {
                    EnableColumn((int)position.x + 1);
                    return;
                }
                if (position.x - 1 != 0)
                    return;
                for (int i = 0; i < chunksGO.Count; i++)
                {
                    GameObject newGO = new GameObject();
                    chunks[i].Insert(0,new Chunk(sizeOfChunk, tilePrefab, new Vector2(chunksGO[i][0].transform.position.x- 1 * sizeOfChunk, -i * sizeOfChunk), chunksGO.Count, ref newGO, out newGO));
                    chunksGO[i].Insert(0,newGO);
                }
            }
           
            
        }
        for (int i = 0; i < chunksGO.Count; i++)
        {
            for (int x = 0; x < chunksGO[i].Count; x++)
            {
                chunksGO[i][x].name = "Chunk " + i + "" + x;
               
            }
        }
    }
    public void Populate(GameObject tilePrefab)
    {
        for (int y = 0; y < numberOfChunkToLoad; y++)
        {
            List<Chunk> chunkRow = new List<Chunk>();
            List<GameObject> chunkRowGO = new List<GameObject>();
            for (int x = 0; x < numberOfChunkToLoad; x++)
            {
                GameObject newGo =new GameObject();
                chunkRow.Add(new Chunk(sizeOfChunk, tilePrefab, new Vector2(x, -y) * sizeOfChunk, chunksGO.Count, ref newGo, out newGo));
                chunkRowGO.Add(newGo);
            }
            chunks.Add(chunkRow);
            chunksGO.Add( chunkRowGO);
        }
        for (int i = 0; i < chunksGO.Count; i++)
        {
            for (int x = 0; x < chunksGO[i].Count; x++)
            {
                chunksGO[i][x].name = "Chunk " + i + "" + x;
            }
        }
    }

    private void DisableRow(int row)
    {
        foreach (var gameObject in chunksGO[row])
        {
            gameObject.SetActive(false);
        }
    }
    private void DisableColumn(int Column)
    {
        for (int i = 0; i < chunksGO.Count; i++)
        {
            chunksGO[i][Column].SetActive(false);
        }
    }
    private void EnableRow(int row)
    {
        foreach (var gameObject in chunksGO[row])
        {
            gameObject.SetActive(false);
        }
    }
    private void EnableColumn(int Column)
    {
        for (int i = 0; i < chunksGO.Count; i++)
        {
            chunksGO[i][Column].SetActive(true);
        }
    }
}
