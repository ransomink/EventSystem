using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewEvent", menuName = "SO/Event/Base", order = 1)]
    public class Event : BaseEvent<Void>
    {
        //public void Raise()
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        //var l = (EventListener)Listeners[ i ];
        //        var l = Listeners[ i ] as EventListener;
        //        l.OnEventRaised();
        //    }
        //}
    }
}
