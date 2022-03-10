using UnityEngine;
[System.Serializable]
public class TileForSave
{
    public int indexParentChunk;
    public Vector2 posInStart;
    public int materialValue ;
    public TileForSave()
    {

    }
    public TileForSave( int _indexParent, Vector2 _posInStart, int _materialValue)
    {
        indexParentChunk = _indexParent;
        posInStart = _posInStart;
        materialValue = _materialValue;
    }
}