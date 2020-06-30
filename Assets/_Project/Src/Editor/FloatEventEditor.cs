using Ransomink.Events;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloatEvent))]
public class FloatEventEditor : Editor
{
    private float labelWidth = 100f;

    private float f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        EditorGUILayout.LabelField("PARAMS", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Float Value", GUILayout.Width(labelWidth));
        f= EditorGUILayout.FloatField("Float Value", f);
        GUILayout.EndHorizontal();

        var e = target as FloatEvent;

        if (GUILayout.Button("RAISE"))
        {
            e.Raise(f);
        }
    }
}
