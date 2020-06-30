using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewMeterEvent", menuName = "SO/Event/Meter", order = 7)]
    public class MeterEvent : BaseEvent<UIMeterArgs>
    {
        //public void Raise()
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        //var l = (MeterEventListener)Listeners[ i ];
        //        var l = Listeners[ i ] as MeterEventListener;
        //        l.OnEventRaised();
        //    }
        //}
    }
}
