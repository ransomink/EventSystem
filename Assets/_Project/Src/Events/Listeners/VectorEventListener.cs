using UnityEngine;

namespace Ransomink.Events
{
    public class VectorEventListener : BaseEventListener<Vector3, VectorEvent, CustomUnityEvent.UnityEventVector>
    {
        //public CustomUnityEvent.UnityEventVector Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!(Event is VectorEvent))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(VectorEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(Vector3 pos)
        //{
        //    Dispatch.Invoke(pos);
        //}
    }
}
