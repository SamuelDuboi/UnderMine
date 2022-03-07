using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{

    public string stringSeed = "seed string";
    public int seed;
    private void Awake()
    {
        seed = stringSeed.GetHashCode();
    }


    public void GenerateSeed(int probablityBitcoin, int probabilityDogeCoin, int probabilityEterum)
    {
      //  byte bitCoinToBit 
    }
}
