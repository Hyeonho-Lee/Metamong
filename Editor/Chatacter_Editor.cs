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

        if (GUILayout.Button("모듈 불러오기"))
        {
            cc.Check_Module();
        }

        if (GUILayout.Button("의상 체인지")) {
            //cc.Change_All();
        }
    }
}