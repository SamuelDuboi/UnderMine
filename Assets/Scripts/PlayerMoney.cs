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
                return myCryptos[i].ChangeValue(0);
        }
        return 0;
    }
    public float GetNumberOwned(int index)
    {
        return myCryptos[index].ChangeValue(0);
    }
    public float ChangeValue(CryptosType cryptos, float value)
    {
        for (int i = 0; i < myCryptos.Count; i++)
        {
            if (myCryptos[i].myCrypto.myCurrency == cryptos)
                return myCryptos[i].ChangeValue(value);
        }
        return 0;
    }
}
[System.Serializable]
public class CryptosInInventory
{
     public Cryptos myCrypto { get; private set; }
     float numberOwned;
    public float ChangeValue(float value)
    {
        numberOwned += value;

        return numberOwned;
    }
    public float SetValue(float value)
    {
        numberOwned = value;
        return numberOwned;
    }
    public CryptosInInventory(Cryptos _mycrypto)
    {
        myCrypto = _mycrypto;
    }
}