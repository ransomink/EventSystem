using UnityEngine;

namespace Ransomink.Stats
{
    [System.Serializable]
    public class Attribute<T>
    {
        [SerializeField] private T    type;
        [SerializeField] private Stat stat;

        public T    Type => type;
        public Stat Stat => stat;
    }
}
