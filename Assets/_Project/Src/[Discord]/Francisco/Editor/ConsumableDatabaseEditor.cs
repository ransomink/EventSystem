using UnityEngine;
using UnityEditor;

public class ConsumableDatabaseEditor : EditorWindow
{
    public const string CONSUMABLE_DATABASE_PATH = @"Assets/Scriptables/Databases/ConsumableDb.asset";

    protected enum EditorState
    {
        BLANK, EDIT, ADD
    }

    protected EditorState state;
    protected int selected;

    protected string new_name;
    protected string new_desc;
    protected Sprite new_icon;
    protected int new_hpGain;
    protected int new_sanityGain;
    protected int new_hydrationGain;
    protected int new_nourishmentGain;
    protected Consumable.EffectType new_effectA;
    protected Consumable.EffectType new_effectB;

    protected Vector2 _scrollPosList;
    protected Vector2 _scrollPosMain;

    ConsumableDatabase consumableDb;

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth( true ) );
        DisplayListArea();
        DisplayMainArea();
        EditorGUILayout.EndHorizontal();
    }

    private void OnEnable()
    {
        state = EditorState.BLANK;
        LoadDatabase( ref consumableDb, CONSUMABLE_DATABASE_PATH );
        //Debug.Log( consumableDb );
    }

    void LoadDatabase( ref ConsumableDatabase database, string path )
    {
        database = ( ConsumableDatabase )AssetDatabase.LoadAssetAtPath( path, typeof( ConsumableDatabase ) );
        //Debug.Log( database );
        if ( database == null )
            CreateDatabase( ref database, path );
    }

    void CreateDatabase( ref ConsumableDatabase database, string path )
    {
        database = CreateInstance<ConsumableDatabase>();
        AssetDatabase.CreateAsset( database, path );
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log( consumableDb );
    }

    void DisplayListArea()
    {
        EditorGUILayout.BeginVertical( GUILayout.Width( 250 ) );
        EditorGUILayout.Space();

        _scrollPosList = EditorGUILayout.BeginScrollView( _scrollPosList, "box", GUILayout.ExpandHeight( true ) );

        for ( int i = 0; i < consumableDb.Database.Count; i++ )
        {
            EditorGUILayout.BeginHorizontal();
            if ( GUILayout.Button( "-", GUILayout.Width( 25 ) ) )
            {
                consumableDb.Database.RemoveAt( i );
                EditorUtility.SetDirty( consumableDb );
                state = EditorState.BLANK;
                return;
            }

            if ( GUILayout.Button( consumableDb.Database[ i ].Name, "box", GUILayout.ExpandWidth( true ) ) )
            {
                selected = i;
                state = EditorState.EDIT;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal( GUILayout.ExpandWidth( true ) );
        EditorGUILayout.LabelField( "Consumables " + consumableDb.Database.Count, GUILayout.Width( 100 ) );

        if ( GUILayout.Button( "New Consumable" ) )
        {
            state = EditorState.ADD;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    void DisplayMainArea()
    {
        EditorGUILayout.BeginVertical( GUILayout.ExpandWidth( true ) );
        EditorGUILayout.Space();
        _scrollPosMain = EditorGUILayout.BeginScrollView( _scrollPosMain, "box", GUILayout.ExpandHeight( true ) );
        switch ( state )
        {
            case EditorState.ADD:
                DisplayAddMainArea();
                break;
            case EditorState.EDIT:
                DisplayEditMainArea();
                break;
            default:
                DisplayBlankMainArea();
                break;
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }
    void DisplayBlankMainArea()
    {
        GUILayout.Label( "_______________________" );
        EditorGUILayout.LabelField( " Add & Edit Consumables" );
        GUILayout.Label( "_______________________" );
    }

    void DisplayEditMainArea()
    {
        EditorGUILayout.LabelField( "SETTINGS", EditorStyles.boldLabel );
        EditorGUILayout.Space();

        consumableDb.Database[ selected ].Icon = ( Sprite )EditorGUILayout.ObjectField( "Icon: ", consumableDb.Database[ selected ].Icon, typeof( Sprite ), false );

        consumableDb.Database[ selected ].Name = EditorGUILayout.TextField( new GUIContent( "Name: " ), consumableDb.Database[ selected ].Name );

        consumableDb.Database[ selected ].Desc = EditorGUILayout.TextField( new GUIContent( "Description: " ), consumableDb.Database[ selected ].Desc );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField( "ATTRIBUTES", EditorStyles.boldLabel );
        EditorGUILayout.Space();

        EditorGUILayout.LabelField( "Hp Gain: " );
        consumableDb.Database[ selected ].HpGain = ( int )EditorGUILayout.Slider( consumableDb.Database[ selected ].HpGain, -100, 100 );

        EditorGUILayout.LabelField( "Sanity Gain: " );
        consumableDb.Database[ selected ].SanityGain = ( int )EditorGUILayout.Slider( consumableDb.Database[ selected ].SanityGain, -100, 100 );

        EditorGUILayout.LabelField( "Hydration Gain: " );
        consumableDb.Database[ selected ].HydrationGain = ( int )EditorGUILayout.Slider( consumableDb.Database[ selected ].HydrationGain, -100, 100 );

        EditorGUILayout.LabelField( "Nourishment Gain: " );
        consumableDb.Database[ selected ].NourishmentGain = ( int )EditorGUILayout.Slider( consumableDb.Database[ selected ].NourishmentGain, -100, 100 );

        consumableDb.Database[ selected ].EffectA = ( Consumable.EffectType )EditorGUILayout.EnumPopup( "Effect Applied", consumableDb.Database[ selected ].EffectA );

        consumableDb.Database[ selected ].EffectB = ( Consumable.EffectType )EditorGUILayout.EnumPopup( "Side-Effect Applied", consumableDb.Database[ selected ].EffectB );

        EditorGUILayout.Space();

        if ( GUILayout.Button( "Done", GUILayout.Width( 100 ) ) )
        {
            EditorUtility.SetDirty( consumableDb );
            state = EditorState.BLANK;
            ResetThings();
        }
    }

    void DisplayAddMainArea()
    {
        EditorGUILayout.LabelField( "SETTINGS", EditorStyles.boldLabel );
        EditorGUILayout.Space();

        new_icon = ( Sprite )EditorGUILayout.ObjectField( "Icon: ", new_icon, typeof( Sprite ), false );
        new_name = EditorGUILayout.TextField( new GUIContent( "Name: " ), new_name );
        new_desc = EditorGUILayout.TextField( new GUIContent( "Decription: " ), new_desc );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField( "ATTRIBUTES", EditorStyles.boldLabel );
        EditorGUILayout.Space();

        EditorGUILayout.LabelField( "Hp Gain: " );
        new_hpGain = ( int )EditorGUILayout.Slider( new_hpGain, -100, 100 );
        EditorGUILayout.LabelField( "Sanity Gain: " );
        new_sanityGain = ( int )EditorGUILayout.Slider( new_sanityGain, -100, 100 );
        EditorGUILayout.LabelField( "Hydration Gain: " );
        new_hydrationGain = ( int )EditorGUILayout.Slider( new_hydrationGain, -100, 100 );
        EditorGUILayout.LabelField( "Nourishment Gain: " );
        new_nourishmentGain = ( int )EditorGUILayout.Slider( new_nourishmentGain, -100, 100 );
        new_effectA = ( Consumable.EffectType )EditorGUILayout.EnumPopup( "Effect Applied", new_effectA );
        new_effectB = ( Consumable.EffectType )EditorGUILayout.EnumPopup( "Side-Effect Applied", new_effectB );

        EditorGUILayout.Space();

        if ( GUILayout.Button( "Done", GUILayout.Width( 100 ) ) )
        {
            consumableDb.Database.Add( new Consumable( new_name, new_desc, new_icon, new_hpGain, new_sanityGain, new_nourishmentGain, new_hydrationGain, new_effectA, new_effectB ) );

            EditorUtility.SetDirty( consumableDb );
            ResetThings();
        }
    }

    void ResetThings()
    {
        new_name = string.Empty;
        new_desc = string.Empty;
        new_icon = null;
        new_hpGain = 0;
        new_sanityGain = 0;
        new_hydrationGain = 0;
        new_nourishmentGain = 0;
        new_effectA = Consumable.EffectType.NONE;
        new_effectB = Consumable.EffectType.NONE;
        state = EditorState.BLANK;
    }
}