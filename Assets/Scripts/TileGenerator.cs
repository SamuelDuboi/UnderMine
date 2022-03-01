using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    Card currentCard = new Card();
    public GameObject tilePrefab;
    public direction direction;
    public static TileGenerator instance;
    private Vector2Int currentChunk;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(gameObject);
    }
    void Start()
    {
        currentCard.Populate(tilePrefab);
        currentChunk = Vector2Int.one ;
    }

    public void CheckPos(Vector2 playerPos)
    {
        currentCard.chunks[currentChunk.y][currentChunk.x].IsWithinChunk(playerPos, out direction);
        if (direction.HasFlag(direction.north))
        {
            if (currentChunk.y != 1)
                currentChunk.y--;
        }
       else if (direction.HasFlag(direction.south))
        {
            currentCard.Populate(true, currentChunk, tilePrefab,-1);
            currentChunk.y++;
        }
         if (direction.HasFlag(direction.west))
        {
            currentCard.Populate(false, currentChunk, tilePrefab, -1);
            if (currentChunk.x != 1)
                currentChunk.x--;
        }
        else if (direction.HasFlag(direction.east))
        {
            currentCard.Populate(false, currentChunk, tilePrefab,1);
            currentChunk.x++;
        }
    }
}
