using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Level level = target as Level;

        DrawDefaultInspector();

        if(GUILayout.Button("Generate"))
        {
            level.GenerateLevel();
        }
        
    }
}
