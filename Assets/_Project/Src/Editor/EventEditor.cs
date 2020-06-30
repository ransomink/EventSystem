using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Event))]
public class EventEditor : Editor
{
    public Void v;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        var e = target as Event;

        if (GUILayout.Button("RAISE"))
        {
            e.Raise(v);
        }
    }
}
