using UnityEngine;

namespace Ransomink.Events
{
    public class MeterEventListener : BaseEventListener<UIMeterArgs, MeterEvent, CustomUnityEvent.UnityEventMeter>
    {
        //public CustomUnityEvent.UnityEventMeter Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!(Event is MeterEvent))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(MeterEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(Meter.Info info)
        //{
        //    Dispatch.Invoke(info);
        //}
    }
}
