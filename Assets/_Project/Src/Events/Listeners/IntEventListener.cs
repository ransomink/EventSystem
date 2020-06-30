using UnityEngine;

namespace Ransomink.Events
{
    public class IntEventListener : BaseEventListener<int, IntEvent, CustomUnityEvent.UnityEventInt>
    {
        //public CustomUnityEvent.UnityEventInt Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!Event.Equals(typeof(IntEvent)))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(IntEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(int value)
        //{
        //    Dispatch.Invoke(value);
        //}
    }
}
