using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CanvasScaler))]
public class CanvasScalerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Set Default Canvas Settings"))
        {
            ((CanvasScaler)target).SetDefaultCanvasSettings();
        }
    }
}