using System.Collections.Generic;
using Ransomink.Framework;
using UnityEngine;

namespace Ransomink.Stats
{
    [CreateAssetMenu(
        fileName = Utility.NEW + Utility.NAME_MOD_TYPE,
        menuName = Utility.SO  + Utility.BASE_STAT + Utility.NAME_PERCENT,
        order    = 1
    )]
    public class PercentAdd : ModType
    {
        public override float CalculateModifiedValue(float final, ref float percent, int index, IReadOnlyList<Modifier> modifiers)
        {
            var nextMod = modifiers[index++];
            var curMod  = modifiers[index];
            percent    += curMod.Value;

            if (index++ >= modifiers.Count || nextMod.Type.name != this.name)
            {
                final  *= 1 + curMod.Value; 
                percent = 0f;
            }

            return final;
        }
    }
}
