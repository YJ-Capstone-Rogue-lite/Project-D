using UnityEngine;
using UnityEditor;
using System.Collections;
 
[CustomEditor(typeof(Room))]
public class RoomTestEditor : Editor {
    override public void OnInspectorGUI() {
        Room colliderCreator = (Room)target;
        if(GUILayout.Button("RoomEnter")) colliderCreator.RoomEnterTrigger();
        if(GUILayout.Button("RoomExit")) colliderCreator.RoomExitTrigger();
        DrawDefaultInspector();
    }
}
