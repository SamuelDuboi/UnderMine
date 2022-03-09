using UnityEngine;
[System.Serializable]
public class Drill 
{
    public int number;
    public Vector2 pos;
    public direction direction;
    public int stratNumber;
    public CryptosType myType;
    public Drill(int _number,int _stratNumber, Vector2 _pos, direction _direction, CryptosType _myType)
    {
        number = _number;
        pos = _pos;
        stratNumber = _stratNumber;
        direction = _direction;
        myType = _myType;
    }
}
