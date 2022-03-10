using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="PlayerMoney")]
[System.Serializable]
public class PlayerMoney : ScriptableObject
{
    [SerializeField]
    public List<CryptosInInventory> myCryptos =new List<CryptosInInventory>();
    public void AddCrypto(Cryptos _crypto)
    {
        for (int i = 0; i < myCryptos.Count; i++)
        {
            if (myCryptos[i].myCrypto == _crypto)
                return ;
        }
        myCryptos.Add( new CryptosInInventory(_crypto));
    }
    public void RemoveCrypto(int index)
    {
        myCryptos.RemoveAt(index);
    }
    public void RemoveCrypto(CryptosInInventory index)
    {
        myCryptos.Remove(index);
    }
    public float GetNumberOwned(CryptosType cryptos)
    {
        for (int i = 0; i < myCryptos.Count; i++)
        {
            if (myCryptos[i].myCrypto.myCurrency == cryptos)
                return myCryptos[i].AddRevenu(0);
        }
        return 0;
    }
    public float GetNumberOwned(int index)
    {
        return myCryptos[index].AddRevenu(0);
    }
    public void ChangeValue(CryptosType cryptos, float value)
    {
        if(myCryptos.Count!= 4)
        {
            for (int i = 0; i < 4; i++)
            {
                myCryptos.Add(new CryptosInInventory(TileGenerator.instance.myCryptos[i]));
            }
        }


        for (int i = 0; i < myCryptos.Count; i++)
        {
            if (myCryptos[i].myCrypto.myCurrency == cryptos)
                 myCryptos[i].ChangeValue(value);
        }
    }
}
[System.Serializable]
public class CryptosInInventory
{
     public Cryptos myCrypto { get; private set; }
     float numberOwned;
     float revenuPerSec;
    /// <summary>
    /// add numberOwned with revenuPerSec
    /// </summary>
    public void AddValue()
    {
        numberOwned += revenuPerSec;
    }
    /// <summary>
    /// add number owned with Value
    /// </summary>
    /// <param name="value"></param>
    public void AddValue(float value)
    {
        numberOwned += value;
    }
    /// <summary>
    /// add revenu per sec with value and return revenuPer sec
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float AddRevenu(float value)
    {
        revenuPerSec += value;
        return revenuPerSec;
    }
    /// <summary>
    /// set number Owned = to value and return it
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float SetValue(float value)
    {
        numberOwned = value;
        return numberOwned;
    }
    /// <summary>
    /// change revenu per sec = value, add number owned by value and return revenu per sec
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public float ChangeValue(float value)
    {
        revenuPerSec = value;
        numberOwned += revenuPerSec;
        return revenuPerSec;
    }

    public float GetRealValuePerSec()
    {
        return revenuPerSec * myCrypto.currentValue;
    }
    public CryptosInInventory(Cryptos _mycrypto)
    {
        myCrypto = _mycrypto;
    }
}