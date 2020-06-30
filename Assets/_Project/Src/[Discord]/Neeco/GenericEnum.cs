//using System;
//using UnityEngine;

//public class GenericEnum<T> : MonoBehaviour where T : Enum
//{
//    private string[] Names;
//    private string[] Values;

//    public void ConvertEnumToList()
//    {
//        Names  = Enum.GetNames ( typeof( T ) );
//        Debug.Log( $"Enum Names: {Names}" );
//    }

//    public void ConvertEnumValueToIndex()
//    {
//        if ( Names.Length == 0 ) ConvertEnumToList();
//    }
//}
