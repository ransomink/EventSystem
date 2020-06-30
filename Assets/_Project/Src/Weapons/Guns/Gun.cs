using System.Collections.Generic;
using Ransomink.Framework;
using Ransomink.Stats;
using UnityEngine;

namespace Ransomink.Weapons
{
    [CreateAssetMenu(
        fileName = Utility.NEW + Utility.NAME_GUN, 
        menuName = Utility.SO  + Utility.CLASS_WEAPON + Utility.NAME_GUN,
        order    = 0
    )]
    public class Gun : ScriptableObject, IGun
    {
        [Header("REFS")]
        [SerializeField] private GunLogic   prefab;
        [SerializeField] private Projectile projectile;

        [Header("STATS")]
        [SerializeField] private List<GunAttribute> attributes;

        [Header("ATTACHMENTS")]
        [SerializeField] private List<GunAttachmentContainer> attachments;

        internal GunLogic   Prefab => prefab;
        internal Projectile Projectile => projectile;
        public IReadOnlyDictionary<GunStatType, Stat> Stats => _stats;

        public void Init()
        {
            foreach (var attribute in attributes)
            {
                _stats[attribute.Type] = attribute.Stat;
            }
        }

        public void Ads()
        {
            
        }

        public void Shoot()
        {
            var clone = Instantiate(Projectile, Prefab.SpawnPoint.position, Quaternion.identity);
        }

        public void Reload()
        {
            
        }

        private Dictionary<GunStatType, Stat> _stats;
    }
}
