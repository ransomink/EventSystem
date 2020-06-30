using System.Collections.Generic;
using UnityEngine;

namespace Francisco
{
    [CreateAssetMenu( fileName = "NewDatabase", menuName = "SO/Francisco/Database" )]
    public class SO_Database : ScriptableObject
    {
        [SerializeField] List<string> Items;

        public bool ItemExists( string name )
        {
            return Items.Contains( name ) ? true : false;
        }
    }
}