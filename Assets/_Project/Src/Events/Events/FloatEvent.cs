using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewFloatEvent", menuName = "SO/Event/Float", order = 3)]
    public class FloatEvent : BaseEvent<float>
    {
        //public void Raise(float value)
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        var l = (FloatEventListener)Listeners[ i ];
        //        l.OnEventRaised(value);
        //    }
        //}
    }
}
