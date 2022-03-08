using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int numberOfChunkToLoad { get => 3; }
    public int sizeOfChunkX { get ;  private set; }
    public int sizeOfChunkY { get ;  private set; }
    public List<Chunk> chunks ;
    private GameObject card;
    public List<Cryptos> myCryptos;
    public void Init(GameObject _card, int chunksizeX,int chunkSizeY, List<Cryptos> crytpos)
    {
        card = _card;
        card.name = "Card";
        sizeOfChunkX = chunksizeX;
        sizeOfChunkY = chunkSizeY;
        myCryptos = crytpos;
    }

    public void Populate(bool vertical, Vector2 position, GameObject tilePrefab,GameObject tileEmptyPrefab, float direction, float sizeOfTiles)
    {
        if (vertical)
        {
            if (direction > 0)
            {
                DisableRow((int)position.y + 1);
                if (position.y - 1 > 0)
                {
                    EnableRow((int)position.y - 2, position.y);
                    return;
                }
            }
            else if (direction < 0)
            {

                DisableRow((int)position.y - 1);
                if (position.y > 1 && position.y + 2 < chunks.Count)
                {
                    EnableRow((int)position.y + 2, position.y);
                    return;
                }

                for (int i = 0; i < 3; i++)
                {
                    GameObject newGO = new GameObject();
                    bool isStone = true;
                    if ((-chunks.Count * sizeOfChunkY) % 2 == 0)
                        isStone = false;
                    var chunk = newGO.AddComponent<Chunk>();
                    chunk.Init(sizeOfChunkX, sizeOfChunkY,i, tilePrefab,tileEmptyPrefab, new Vector2(chunks[0].gameObject.transform.position.x, -chunks.Count * sizeOfChunkY), isStone, sizeOfTiles, myCryptos, i, ref newGO, out newGO);
                    if (!chunks[(int)position.y*3+i].gameObject.activeSelf)
                        newGO.SetActive(false);
                    newGO.transform.SetParent(transform);
                    chunks.Add(chunk);
                }
            }

        }
        //not used for now (until gd change mind again)
        /* else
         {
             if (direction > 0)
             {
                 DisableColumn((int)position.x - 1);
                 if (position.x + 2 < chunks[0].chunksGO.Count)
                 {
                     EnableColumn((int)position.x + 2, position.x);
                     return;
                 }
                 int xValue = chunks[0].chunksGO.Count;
                 for (int i = 0; i < chunks.Count; i++)
                 {
                     GameObject newGO = new GameObject();
                     bool isStone = true;
                     if (i * sizeOfChunkY % 2 == 0)
                         isStone = false;
                     var chunk = newGO.AddComponent<Chunk>();
                     chunk.Init(sizeOfChunkX, sizeOfChunkY, tilePrefab, new Vector2(chunks[i].chunksGO[xValue - 1].transform.position.x + 1 * sizeOfChunkX, -i * sizeOfChunkY), isStone, sizeOfTiles, myCryptos,i, ref newGO, out newGO);
                     chunks[i].chunks.Add(chunk);
                     if (!chunks[i].chunksGO[(int)position.x].activeSelf)
                         newGO.SetActive(false);
                     newGO.transform.SetParent(card.transform);
                     chunks[i].chunksGO.Add(newGO);
                 }
             }
             else
             {
                 DisableColumn((int)position.x + 1);
                 if (position.x - 1 > 0)
                 {
                     EnableColumn((int)position.x - 2, position.x);
                     return;
                 }

                 for (int i = 0; i < chunks.Count; i++)
                 {
                     GameObject newGO = new GameObject();
                     bool isStone = true;
                     if ((-i * sizeOfChunkY) % 2 == 0)
                         isStone = false;
                     var chunk = newGO.AddComponent<Chunk>();
                     chunk.Init(sizeOfChunkX, sizeOfChunkY, tilePrefab, new Vector2(chunks[i].chunks[0].transform.position.x - 1 * sizeOfChunkX, -i * sizeOfChunkY), isStone, sizeOfTiles, myCryptos, i, ref newGO, out newGO);
                     chunks[i].chunks.Insert(0, chunk);
                     if (!chunks[i].chunksGO[(int)position.x].activeSelf)
                         newGO.SetActive(false);
                     newGO.transform.SetParent(card.transform);
                     chunks[i].chunksGO.Insert(0, newGO);
                 }
             }


         }*/
        for (int i = 0; i < chunks.Count; i++)
        {
            chunks[i].name = "Chunk " + i + "" + i % 3;
        }
    }
    public void Populate(GameObject tilePrefab, float sizeOfTiles, GameObject tileEmptyPrefab)
    {
        chunks = new List<Chunk>();
        for (int y = 0; y < numberOfChunkToLoad+5; y++)
        {
            for (int x = 0; x < numberOfChunkToLoad; x++)
            {
                GameObject newGo = new GameObject();
                bool isStone = true;
                float valueOfy = y;
                if (y % 2 == 0)
                    isStone = false;
                if (y == 2)
                    valueOfy = 8f / 3f;
                var chunk = newGo.AddComponent<Chunk>();
                var realSizeOfChunkX = sizeOfChunkX;
               if(x==0)
                chunk.Init(10, sizeOfChunkY,chunks.Count, tilePrefab, tileEmptyPrefab, new Vector2(0 , -((sizeOfChunkY+1)*y)) , isStone, sizeOfTiles, myCryptos,y, ref newGo, out newGo);
                else if (x == 1)
                    chunk.Init(sizeOfChunkX, sizeOfChunkY, chunks.Count, tilePrefab, tileEmptyPrefab, new Vector2(10, -((sizeOfChunkY + 1) * y)), isStone, sizeOfTiles, myCryptos, y, ref newGo, out newGo);
                else if (x == 2)
                    chunk.Init(10, sizeOfChunkY, chunks.Count, tilePrefab, tileEmptyPrefab, new Vector2(10+ sizeOfChunkX, -((sizeOfChunkY + 1) * y)), isStone, sizeOfTiles, myCryptos, y, ref newGo, out newGo);
                newGo.transform.SetParent(card.transform);
                chunks.Add(chunk);
            }
        }
        for (int i = 0; i < chunks.Count; i++)
        {
                chunks[i].name = "Chunk " + i + "" + i%3;
        }
    }

    private void DisableRow(int row)
    {
        for (int i = row; i < row+3; i++)
        {
            chunks[i].gameObject.SetActive(false);
        }
    }
    private void DisableColumn(int Column)
    {
        for (int i = 0; i < chunks.Count/3; i++)
        {
            chunks[i*3+Column].gameObject.SetActive(false);
        }
    }
    private void EnableRow(int row, float pos)
    {
       /* for (int x = 0; x < chunks[row].chunksGO.Count; x++)
        {
            if (chunks[(int)pos].chunksGO[x].activeSelf)
                chunks[row].chunksGO[x].SetActive(true);
        }*/
    }

    private void EnableColumn(int Column, float pos)
    {
       /* for (int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i].chunksGO[(int)pos].activeSelf)
                chunks[i].chunksGO[Column].SetActive(true);
        }*/
    }


}
