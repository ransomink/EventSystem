using System.Collections.Generic;
using Ransomink.Framework;
using UnityEngine;

namespace Ransomink.Weapons
{
    [CreateAssetMenu(
        fileName = Utility.NEW + Utility.NAME_GUN  + Utility.NAME_ATTACMENT, 
        menuName = Utility.SO  + Utility.BASE_ATTACHMENT + Utility.CLASS_WEAPON + Utility.NAME_GUN,
        order    = 0
    )]
    public class GunAttachment : ScriptableObject
    {
        [SerializeField] private List<BaseGunAttachment> attachments;

        public IReadOnlyList<BaseGunAttachment> Attachments => attachments.AsReadOnly();
    }
}
