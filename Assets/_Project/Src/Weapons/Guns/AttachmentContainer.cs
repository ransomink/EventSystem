using System.Collections.Generic;
using UnityEngine;

namespace Ransomink.Weapons
{
    public class AttachmentContainer<TType, TAttachment>
    {
        [SerializeField] private TType       type;
        [SerializeField] private TAttachment attachment;

        public TType       Type       => type;
        public TAttachment Attachment => attachment;
    }
}
