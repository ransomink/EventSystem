using UnityEngine;

namespace Ransomink.Events
{
    [CreateAssetMenu(fileName = "NewGameObjectEvent", menuName = "SO/Event/GameObject", order = 6)]
    public class GameObjectEvent : BaseEvent<GameObject>
    {
        //public void Raise(GameObject value)
        //{
        //    for (var i = Listeners.Count - 1; i >= 0; i--)
        //    {
        //        var l = (GameObjectEventListener)Listeners[ i ];
        //        l.OnEventRaised(value);
        //    }
        //}
    }
}
