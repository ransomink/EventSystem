using System;

namespace Ransomink.Framework
{
    [Serializable]
    public static class Utility
    {
        // NEW
        public const string NEW = "New";

        // TYPE
        public const string SO  = "SO/";

        // BASE
        public const string BASE_EVENT       = "Event/";
        public const string BASE_STAT        = "Stats/";
        public const string BASE_ATTACHMENT = "Attachments/";

        // CLASS
        public const string CLASS_CHARACTER = "Character";
        public const string CLASS_WEAPON    = "Weapon/";

        // SUB-CLASS
        public const string SUB_GUN = "Gun/";

        // DEFINED CLASS
        #region BASE
        public const string NAME_ATTACMENT  = "Attachment";
        public const string NAME_ATTRIBUTE  = "Attribute";
        #endregion
        #region EVENTS
        public const string NAME_AIRBORNE   = "Airborne";
        public const string NAME_BASE       = "Base";
        public const string NAME_BOOL       = "Bool";
        public const string NAME_COLLISION  = "Collision";
        public const string NAME_FLOAT      = "Float";
        public const string NAME_GAMEOBJECT = "GameObject";
        public const string NAME_INT        = "Int";
        public const string NAME_METER      = "Meter";
        public const string NAME_MOVEMENT   = "Movement";
        public const string NAME_STRING     = "String";
        public const string NAME_VECTOR     = "Vector";
        public const string NAME_WALL       = "Wall";
        #endregion
        #region MODTYPE
        public const string NAME_MOD_TYPE  = "ModType";
        #endregion
        #region WEAPONS
        public const string NAME_GUN = "Gun";
        #endregion
    }
}
