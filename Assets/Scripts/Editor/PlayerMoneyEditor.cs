using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerMoney))]
public class PlayerMoneyEditor : Editor
{
    private PlayerMoney playerMoney;
    private List<Cryptos> myCryptos;
    private List<float> value;
    private void OnEnable()
    {
        playerMoney = target as PlayerMoney;
        myCryptos = new List<Cryptos>();
        value = new List<float>();
        foreach (CryptosInInventory item in playerMoney.myCryptos)
        {
            myCryptos.Add(item.myCrypto);

            value.Add(item.ChangeValue(0));
        }
    }

    public override void OnInspectorGUI()
    {
      
        for (int i = 0; i < myCryptos.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            myCryptos[i] = (Cryptos)EditorGUILayout.ObjectField(myCryptos[i], typeof(Cryptos), true);
            if (EditorGUI.EndChangeCheck()) playerMoney.AddCrypto(myCryptos[i]);
            EditorGUI.BeginChangeCheck();
            value[i] = EditorGUILayout.FloatField(value[i]);
            if (EditorGUI.EndChangeCheck())
            {
                playerMoney.myCryptos[i].SetValue(value[i]);

            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("+"))
        {
            myCryptos.Add(null);
            value.Add(0);
        }

        EditorUtility.SetDirty(playerMoney);
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
