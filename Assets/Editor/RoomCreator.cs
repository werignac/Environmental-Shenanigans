using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class RoomCreator : MonoBehaviour
{
    [MenuItem("ESTools/SaveRooms")]
    static void SaveRooms()
    {
        File.Delete("Data/RoomData.csv");
        StreamWriter writer = new StreamWriter("Data/RoomData.csv");
        writer.WriteLine("Name,Width,RoomType,NextRoomType");
        string[] assets = AssetDatabase.FindAssets("", new string[] { "Assets/Resources/Rooms" });
        foreach(string guid in assets)
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(guid));
            GameObject gO = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
            writer.WriteLine(gO.GetComponent<RoomData>().GetRoomData());
        }
        writer.Close();
    }
}
