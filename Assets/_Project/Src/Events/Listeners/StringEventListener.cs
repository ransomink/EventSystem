using UnityEngine;

namespace Ransomink.Events
{
    public class StringEventListener : BaseEventListener<string, StringEvent, CustomUnityEvent.UnityEventString>
    {
        //public CustomUnityEvent.UnityEventString Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!(Event is StringEvent))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(StringEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(string value)
        //{
        //    Dispatch.Invoke(value);
        //}
    }
}
