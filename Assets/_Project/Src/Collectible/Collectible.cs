using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEngine;

namespace Ransomink.Collectible
{
    public abstract class Collectible : MonoBehaviour, ICollectible
    {
        [SerializeField] protected int value;
        [SerializeField] protected int multiplier = 1;
        [SerializeField] protected CollectibleGroup group;

        [Header("EVENTS")]
        [SerializeField] protected IntEvent OnCollect;
        [SerializeField] protected Event    OnCollectibleGroupAdd;

        private Void _void;

        public int Value
        {
                      get => value * Multiplier;
            protected set => value = this.value;
        }

        public int Multiplier => multiplier;

        public CollectibleGroup Group => group;

        public void SetGroup(CollectibleGroup cg)
        {
            cg = group;
            OnCollectibleGroupAdd.Raise(_void);
        }

        public void Collect() => OnCollect.Raise(Value);

        private void CheckTag(Component comp)
        {
            if (comp.CompareTag("Player")) Collect();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CheckTag(other);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            CheckTag(other);
        }
    }
}
