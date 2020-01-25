using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomData))]
public class RoomDataInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Save Room"))
        {
            PrefabUtility.SaveAsPrefabAsset(((RoomData)target).gameObject, "Assets/Resources/Rooms/" + ((RoomData)target).roomName + ".prefab");
        }
    }

}
