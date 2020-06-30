using UnityEditor;

public class EditorWindows : EditorWindow
{
    [MenuItem( "Databases/ConsumableDatabase" )]
    static void Init()
    {
        EditorWindow window = GetWindow( typeof(ConsumableDatabaseEditor) );
        window.Show();
    }
}