using UnityEngine;

namespace Ransomink.Events
{
    public class GameObjectEventListener : BaseEventListener<GameObject, GameObjectEvent, CustomUnityEvent.UnityEventGameObject>
    {
        //public CustomUnityEvent.UnityEventGameObject Dispatch;

        //public override void OnValidate()
        //{
        //    if (Event == null) return;

        //    if (!(Event is GameObjectEvent))
        //    {
        //        Debug.LogError($"{Event.name} ({Event.GetType()}) does not match Type ({typeof(GameObjectEvent)})");
        //        Event = null;
        //    }
        //    else
        //    {
        //        Debug.Log($"{Event.name} is of Type ({Event.GetType()})");
        //    }
        //}

        //public void OnEventRaised(GameObject go)
        //{
        //    Dispatch.Invoke(go);
        //}
    }
}
