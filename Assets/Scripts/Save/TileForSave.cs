using UnityEngine;
[System.Serializable]
public class TileForSave
{
    public int indexParentChunk;
    public Vector2 posInStart;
    public bool isStone;
    public TileForSave()
    {

    }
    public TileForSave( int _indexParent, Vector2 _posInStart, bool _isStone)
    {
        indexParentChunk = _indexParent;
        posInStart = _posInStart;
        isStone = _isStone;
    }
}