using Ransomink.Stats;
using UnityEngine;

namespace Ransomink.Weapons
{
    public class Attachment<T>
    {
        [SerializeField] private T        type;
        [SerializeField] private Modifier modifier;

        public T        Type     => type;
        public Modifier Modifier => modifier;
    }
}
