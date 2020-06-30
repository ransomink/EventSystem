using System.Collections.Generic;
using Ransomink.Stats;

namespace Ransomink.Weapons
{
    public interface IGun
    {
        IReadOnlyDictionary<GunStatType, Stat> Stats { get; }

        void Init();
        void Ads();
        void Shoot();
        void Reload();
    }
}
