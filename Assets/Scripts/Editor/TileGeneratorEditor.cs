using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TileGenerator))]
public class TileGeneratorEditor : Editor
{
    private TileGenerator generatorTarget;

    private void OnEnable()
    {
        generatorTarget = target as TileGenerator;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       // EditorGUILayout.LabelField(generatorTarget.currentCard.sizeOfChunkX.ToString());
        if (GUILayout.Button("Generate")) generatorTarget.Populate();
    }
}
