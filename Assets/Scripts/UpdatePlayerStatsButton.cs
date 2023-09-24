using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerController))]
public class UpdatePlayerStatsButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerController pc = (PlayerController)target;
        if (GUILayout.Button("Update Player Stats"))
        {
            pc.UpdateStats();
            Debug.Log("Updated Stats!");
        }
    }
}
