using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Customize_Character))]
public class Chatacter_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Customize_Character cc = (Customize_Character)target;

        if (GUILayout.Button("��� �ҷ�����"))
        {
            cc.Check_Module();
        }

        if (GUILayout.Button("�ǻ� ü����")) {
            //cc.Change_All();
        }
    }
}