using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewIntEvent", menuName = "SO/Event/Int", order = 0)]
    public class IntEvent : BaseEvent<int>
    {
        public override bool Equals(object other)
        {
            var e = other as System.Type;
            return GetType().Equals(e);
        }

        public static bool operator ==(IntEvent l, IntEvent r)
        {
            return true;
        }

        public static bool operator !=(IntEvent l, IntEvent r)
        {
            return false;
        }

        //public void Raise(int value)
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        var l = Listeners[ i ] as IntEventListener;
        //        l.OnEventRaised(value);
        //    }
        //}
    }
}
