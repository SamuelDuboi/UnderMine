using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    Card currentCard ;
    public GameObject tilePrefab;
    public direction direction;
    public static TileGenerator instance;
    private Vector2Int currentChunk;
    public List<Cryptos> myCryptos;
    public float sizeOfTile = 1;
    public int numberOfTilesPerChunkX = 12;
    public int numberOfTilesPerChunkY = 12;
    public float globalMultiplicator = 0.5f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    void Start()
    {
        currentCard = new Card(new GameObject(),numberOfTilesPerChunkX,numberOfTilesPerChunkY, myCryptos);
        currentCard.Populate(tilePrefab, sizeOfTile);
        currentChunk = Vector2Int.right;
    }

    public void CheckPos(Vector2 playerPos)
    {
        currentCard.chunks[currentChunk.y][currentChunk.x].IsWithinChunk(playerPos, out direction);
        if (direction.HasFlag(direction.north))
        {
            if (currentChunk.y > 1)
            {
                currentCard.Populate(true, currentChunk, tilePrefab, 1, sizeOfTile);
                currentChunk.y--;
            }
        }
        else if (direction.HasFlag(direction.south))
        {
            if (currentChunk.y > 0)
                currentCard.Populate(true, currentChunk, tilePrefab, -1, sizeOfTile);
            currentChunk.y++;
        }
        if (direction.HasFlag(direction.west))
        {
            currentCard.Populate(false, currentChunk, tilePrefab, -1, sizeOfTile);
            if (currentChunk.x != 1)
                currentChunk.x--;
        }
        else if (direction.HasFlag(direction.east))
        {
            currentCard.Populate(false, currentChunk, tilePrefab, 1, sizeOfTile);
            currentChunk.x++;
        }
    }
    public Vector3 GetSize()
    {
        return new Vector3( currentCard.sizeOfChunkX*sizeOfTile, currentCard.sizeOfChunkY * sizeOfTile,0) ;
    }
}
