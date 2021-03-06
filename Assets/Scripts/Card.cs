using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int numberOfChunkToLoad { get => 3; }
    public int sizeOfChunk { get ;  private set; }
    public List<List<Chunk>> chunks = new List<List<Chunk>>();
    public List<List<GameObject>> chunksGO = new List<List<GameObject>>();
    private GameObject card;
    public List<Cryptos> myCryptos;
    public Card(GameObject _card, int chunksize, List<Cryptos> crytpos)
    {
        card = _card;
        card.name = "Card";
        sizeOfChunk = chunksize;
        myCryptos = crytpos;
    }
    public void Populate(bool vertical, Vector2 position, GameObject tilePrefab, float direction, float sizeOfTiles)
    {
        if (vertical)
        {
            List<Chunk> chunkRow = new List<Chunk>();
            List<GameObject> chunkRowGO = new List<GameObject>();
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
                if (position.y > 1 && position.y + 2 < chunksGO.Count)
                {
                    EnableRow((int)position.y + 2, position.y);
                    return;
                }


                for (int i = 0; i < chunksGO[0].Count; i++)
                {
                    GameObject newGO = new GameObject();
                    chunkRow.Add(new Chunk(sizeOfChunk, tilePrefab, new Vector2(chunksGO[0][i].transform.position.x, -chunksGO.Count * sizeOfChunk), chunksGO.Count, sizeOfTiles, myCryptos, ref newGO, out newGO));
                    if (!chunksGO[(int)position.y][i].activeSelf)
                        newGO.SetActive(false);
                    newGO.transform.SetParent(card.transform);
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
                if (position.x + 2 < chunksGO[0].Count)
                {
                    EnableColumn((int)position.x + 2, position.x);
                    return;
                }
                int xValue = chunksGO[0].Count;
                for (int i = 0; i < chunksGO.Count; i++)
                {
                    GameObject newGO = new GameObject();
                    chunks[i].Add(new Chunk(sizeOfChunk, tilePrefab, new Vector2(chunksGO[i][xValue - 1].transform.position.x + 1 * sizeOfChunk, -i * sizeOfChunk), chunksGO.Count, sizeOfTiles, myCryptos, ref newGO, out newGO));
                    if (!chunksGO[i][(int)position.x].activeSelf)
                        newGO.SetActive(false);
                    newGO.transform.SetParent(card.transform);
                    chunksGO[i].Add(newGO);
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

                for (int i = 0; i < chunksGO.Count; i++)
                {
                    GameObject newGO = new GameObject();
                    chunks[i].Insert(0, new Chunk(sizeOfChunk, tilePrefab, new Vector2(chunksGO[i][0].transform.position.x - 1 * sizeOfChunk, -i * sizeOfChunk), chunksGO.Count, sizeOfTiles, myCryptos, ref newGO, out newGO));
                    if (!chunksGO[i][(int)position.x].activeSelf)
                        newGO.SetActive(false);
                    newGO.transform.SetParent(card.transform);
                    chunksGO[i].Insert(0, newGO);
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
    public void Populate(GameObject tilePrefab, float sizeOfTiles)
    {
        for (int y = 0; y < numberOfChunkToLoad; y++)
        {
            List<Chunk> chunkRow = new List<Chunk>();
            List<GameObject> chunkRowGO = new List<GameObject>();
            for (int x = 0; x < numberOfChunkToLoad; x++)
            {
                GameObject newGo = new GameObject();
                chunkRow.Add(new Chunk(sizeOfChunk, tilePrefab, new Vector2(x, -y) * sizeOfChunk, chunksGO.Count, sizeOfTiles, myCryptos, ref newGo, out newGo));
                newGo.transform.SetParent(card.transform);
                chunkRowGO.Add(newGo);
            }
            chunks.Add(chunkRow);
            chunksGO.Add(chunkRowGO);
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
    private void EnableRow(int row, float pos)
    {
        for (int x = 0; x < chunksGO[row].Count; x++)
        {
            if (chunksGO[(int)pos][x].activeSelf)
                chunksGO[row][x].SetActive(true);
        }
    }

    private void EnableColumn(int Column, float pos)
    {
        for (int i = 0; i < chunksGO.Count; i++)
        {
            if (chunksGO[i][(int)pos].activeSelf)
                chunksGO[i][Column].SetActive(true);
        }
    }


}
