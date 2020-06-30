using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewBoolEvent", menuName = "SO/Event/Bool", order = 2)]
    public class BoolEvent : BaseEvent<bool>
    {
        //public void Raise(bool value)
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        var l = (BoolEventListener)Listeners[ i ];
        //        l.OnEventRaised(value);
        //    }
        //}
    }
}
