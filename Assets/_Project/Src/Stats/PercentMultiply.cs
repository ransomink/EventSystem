using System.Collections.Generic;
using Ransomink.Framework;
using UnityEngine;

namespace Ransomink.Stats
{
    [CreateAssetMenu(
        fileName = Utility.NEW + Utility.NAME_MOD_TYPE,
        menuName = Utility.SO  + Utility.BASE_STAT + Utility.NAME_MULTIPLY,
        order    = 2
    )]
    public class PercentMultiply : ModType
    {
        public override float CalculateModifiedValue(float final, ref float percent, int index, IReadOnlyList<Modifier> modifiers)
        {
            var value = modifiers[index].Value;
            return final * (1 + value);
        }
    }
}
