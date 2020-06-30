using UnityEngine;

namespace Ransomink.Events
{
    public class FloatEventListener : BaseEventListener<float, FloatEvent, CustomUnityEvent.UnityEventFloat>
    {
        //public CustomUnityEvent.UnityEventFloat Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!(Event is FloatEvent))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(FloatEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(float value)
        //{
        //    Dispatch.Invoke(value);
        //}
    }
}
