using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConsumableDatabase : ScriptableObject
{
    [SerializeField]
    List<Consumable> database;

    public List<Consumable> Database
    {
        get { return  database; }
        set { database = value; }
    }

    void OnEnable()
    {
        if ( database == null ) database = new List<Consumable>();
    }

    public bool Exists( Consumable consumable )
    {
        return database.Contains( consumable ) ? true : false;
        //return database.Exists( c => c == consumable );
    }

    public Consumable GetConsumable( string name )
    {
        return database.Find( c => c.Name == name );
    }
}