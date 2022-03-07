using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tile : MonoBehaviour
{
    public Sprite currentSprite;
    public Cryptos cryptoType;
    public int strat;
    public Vector2 position;
    public bool isDigged;
    public float DiggingState;
    public int indexParentChunk;
    public bool isStone;
    public void Init (float diggingState, Cryptos _cryptoType, Vector2 _position, int _indexParentChunk, bool _isStone)
    {
        DiggingState = diggingState;
        cryptoType = _cryptoType;
        position = _position;
        position.x--;
        indexParentChunk = _indexParentChunk;
        isStone = _isStone;
    }


    /// <summary>
    /// return true if is digged
    /// </summary>
    /// <param name="deltaTime"> delta time to update digging state</param>
    /// <returns></returns>
    public float Dig(float deltaTime)
    {
        if (DiggingState > 0)
            DiggingState -= deltaTime;
        else
            isDigged = true;
        return DiggingState;
    }
    public void Select()
    {

    }


}
