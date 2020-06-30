using UnityEngine;
using UnityEngine.Events;

namespace Ransomink.Events
{
    public abstract class BaseEventListener<T, E, UE> : MonoBehaviour, IEventListener<T> where E : BaseEvent<T> where UE : UnityEvent<T>
    {
        [SerializeField] protected E  Event;
        [SerializeField] protected UE Dispatch;

        //public virtual void OnValidate() { }

        private void OnEnable()
        {
            if (Event == null) return;

            Event.Subscribe(this);
        }

        private void OnDisable()
        {
            if (Event == null) return;

            Event.Unsubscribe(this);
        }

        public void OnEventRaised(T value)
        {
            Dispatch.Invoke(value);
        }
    }
}
