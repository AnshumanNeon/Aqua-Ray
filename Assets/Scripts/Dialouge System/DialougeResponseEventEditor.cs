using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialougeResponseEvent))]
public class DialougeResponseEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialougeResponseEvent responseEvents = (DialougeResponseEvent)target;

        if(GUILayout.Button("Refresh"))
        {
            responseEvents.OnValidate();
        }
    }
}
