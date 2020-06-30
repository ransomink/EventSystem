using System.Collections.Generic;
using UnityEngine;

namespace Ransomink.Events
{
    public abstract class BaseEvent<T> : ScriptableObject
    {
        // Each listener of the event will add itself to the list
        [SerializeField] protected List<IEventListener<T>> Listeners = new List<IEventListener<T>>();

        // The event gets called on every listener
        public virtual void Raise(T item)
        {
            for (var i = Listeners.Count - 1; i >= 0; i--)
            {
                Listeners[ i ].OnEventRaised(item);
            }
        }

        public void Subscribe(IEventListener<T> l)
        {
            //if (!Listeners.Contains(l)) Listeners.Add(l);
            if (!Listeners.Contains(l))
            {
                for (var i = 0; i < Listeners.Count; i++)
                {
                    if (Listeners[ i ] == null)
                    {
                        Listeners[ i ] = l;
                        return;
                    }
                }

                Listeners.Add(l);
            }
        }

        public void Unsubscribe(IEventListener<T> l)
        {
            //if (Listeners.Contains(l)) Listeners.Remove(l);
            if (Listeners.Contains(l))
            {
                var i = Listeners.IndexOf(l);
                Listeners[ i ] = null;
            }
        }
    }
}
