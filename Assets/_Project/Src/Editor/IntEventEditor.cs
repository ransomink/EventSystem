using Ransomink.Events;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntEvent))]
public class IntEventEditor : Editor
{
    private float labelWidth = 50f;

    private int i;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        EditorGUILayout.LabelField("PARAMS", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        i = EditorGUILayout.IntField("Int Value", i);
        GUILayout.EndHorizontal();

        var e = target as IntEvent;

        if (GUILayout.Button("RAISE"))
        {
            e.Raise(i);
        }
    }
}
