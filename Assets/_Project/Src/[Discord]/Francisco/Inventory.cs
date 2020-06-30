using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Francisco
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;
        public const string CONSUMABLE_DATABASE_PATH = @"Assets/Scripts/Databases/ConsumableDB.asset";
        public const string WEAPON_DATABASE_PATH     = @"Assets/Scripts/Databases/WeaponDB.asset";

        [SerializeField] Transform  parent;
        [SerializeField] ItemSlot[] itemSlots;
        [SerializeField] List<Item> inventory = new List<Item>();
        [SerializeField] List<SO_Database> database = new List<SO_Database>();

        private void OnValidate()
        {
            if ( parent != null ) itemSlots = parent.GetComponentsInChildren<ItemSlot>();
        }

        private void Start()
        {
            if ( Instance == null )
            {
                Instance = this;
            }
            else
            {
                Destroy( gameObject );
            }

            // You can load individual databases this way but it's much
            // easier to create a string list/array and insert the path
            // in each index and iterate through the list to add a database
            // instead of editing the script every time you have a new database
            LoadDatabase( CONSUMABLE_DATABASE_PATH );
            LoadDatabase( WEAPON_DATABASE_PATH );
        }

        // Load an item database into a list given its path (specific database entry)
        private void LoadDatabase( string path )
        {
            // I assumed your database scriptable objects are just a list of names for each item
            // so I created a database scriptable object with a string list. This SO can be created
            // from the menu and renamed for a specific database.
            var db = (SO_Database)AssetDatabase.LoadAssetAtPath( path, typeof(SO_Database) );
            Debug.Log( database );

            // Check if database asset is found and does not exist in the database list?
            if ( database != null && !database.Contains( db ) ) database.Add( db );
        }

        //Create an item database
        //This will not work because we can't identify a specific
        //item database unless we use a dictionary to store them
        //private void CreateDatabase()
        //{
        //    var db = ScriptableObject.CreateInstance<Francisco_SO_Database>();
        //}

        private void UpdateUI( Item item )
        {
            for ( var i = 0; i < itemSlots.Length; i++ )
            {
                // If the current item slot's item is null, set the added item in the item slot
                if ( itemSlots[ i ].Item == null )
                {
                    itemSlots[ i ].Item = item;
                    return;
                }
            }

            Debug.LogError( "No Empty Slots Exist. Inventory Full" );
        }

        public void AddItem( Item item )
        {
            for ( var i = 0; i < database.Count; i++ )
            {
                if ( database[ i ].ItemExists( item.Name ) )
                {
                    inventory.Add( item );
                    UpdateUI( item );
                    Debug.Log( $"Item Added: {item.Name}" );
                    break;
                }
            }
        }
    }
}