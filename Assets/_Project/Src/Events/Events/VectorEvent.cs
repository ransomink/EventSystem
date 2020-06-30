using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewVectorEvent", menuName = "SO/Event/Vector", order = 5)]
    public class VectorEvent : BaseEvent<Vector3>
    {
        //public void Raise(Vector3 value)
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        var l = (VectorEventListener)Listeners[ i ];
        //        l.OnEventRaised(value);
        //    }
        //}
    }
}
