using UnityEngine;

namespace Ransomink.Events
{
    public class BoolEventListener : BaseEventListener<bool, BoolEvent, CustomUnityEvent.UnityEventBool>
    {
        //public CustomUnityEvent.UnityEventBool Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!Event.Equals(typeof(BoolEvent)))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(BoolEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(bool value)
        //{
        //    Dispatch.Invoke(value);
        //}
    }
}
