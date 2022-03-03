using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile 
{
    public Sprite currentSprite;
    public Cryptos cryptoType{ get; private set; }
    public bool isDigged { get; private set; }
    private float DiggingState { get;  set; }
    public Tile (float diggingState, Cryptos _cryptoType)
    {
        DiggingState = diggingState;
        cryptoType = _cryptoType;
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
