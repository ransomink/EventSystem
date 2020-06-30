using Ransomink.Framework;
using UnityEngine;

namespace Ransomink.Stats
{
    [CreateAssetMenu(
        fileName = Utility.NEW + Utility.NAME_MOD_TYPE, 
        menuName = Utility.SO  + Utility.BASE_STAT + Utility.NAME_MOD_TYPE,
        order    = 0
    )]
    public class ModType : ScriptableObject
    {
        [SerializeField] private int order;

        public int Order => order;
    }
}
