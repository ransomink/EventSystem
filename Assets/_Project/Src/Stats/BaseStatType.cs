using Ransomink.Framework;
using UnityEngine;

namespace Ransomink.Stats
{
    [CreateAssetMenu(
        fileName = Utility.NEW + Utility.NAME_GUN  + Utility.NAME_ATTRIBUTE, 
        menuName = Utility.SO  + Utility.BASE_STAT + Utility.CLASS_WEAPON + Utility.NAME_GUN,
        order    = 0
    )]
    public abstract class BaseStatType : ScriptableObject, IStatType
    {
        
    }
}
