using Ransomink.Events;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeterEvent))]
public class MeterEventEditor : Editor
{
    private bool fold     = true;
    private string status = "Damage";
    private UIMeterArgs args;

    private void OnEnable()
    {
        args = new UIMeterArgs();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        EditorGUILayout.LabelField("PARAMS", EditorStyles.boldLabel);

        fold = EditorGUILayout.Foldout(fold, status);
        if (fold)
        {
            args.flag   = EditorGUILayout.Toggle("Flag", args.flag);
            args.value  = EditorGUILayout.FloatField("Value", args.value);
            args.sender = (GameObject)EditorGUILayout.ObjectField("Sender", args.sender, typeof(GameObject), false);
        }

        var e = target as MeterEvent;

        if (GUILayout.Button("RAISE"))
        {
            e.Raise(args);
        }
    }
}
