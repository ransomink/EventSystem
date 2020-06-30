using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewStringEvent", menuName = "SO/Event/String", order = 4)]
    public class StringEvent : BaseEvent<string>
    {
        //public void Raise(string value)
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        var l = (StringEventListener)Listeners[ i ];
        //        l.OnEventRaised(value);
        //    }
        //}
    }
}
