using UnityEngine;
using UnityEngine.Events;

namespace Ransomink.Events
{
    public class EventListener : BaseEventListener<Void, Event, CustomUnityEvent.UnityEventVoid>
    {
        //public UnityEvent Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    //Debug.Log($"Reference Equals:  {ReferenceEquals(Event.GetType(), typeof(Event))}");
        //    //Debug.Log($"Type Equals:         {Event.Equals(typeof(Event))}");
        //    //Debug.Log($"Equality Operator: {Event.GetType() == typeof(Event)}");
        //    //Debug.Log($"Object Equals:       {Equals(Event, typeof(Event))}");

        //    if (!ReferenceEquals(Event.GetType(), typeof(Event)))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(Event)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised()
        //{
        //    Dispatch.Invoke();
        //}
    }
}
