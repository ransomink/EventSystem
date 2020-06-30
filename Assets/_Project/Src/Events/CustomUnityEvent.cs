using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ransomink.Events
{
    public static class CustomUnityEvent
    {
        // Unity data
        [Serializable] public class UnityEventInt        : UnityEvent<int>        {}
        [Serializable] public class UnityEventVoid       : UnityEvent<Void>       {}
        [Serializable] public class UnityEventBool       : UnityEvent<bool>       {}
        [Serializable] public class UnityEventFloat      : UnityEvent<float>      {}
        [Serializable] public class UnityEventString     : UnityEvent<string>     {}
        [Serializable] public class UnityEventVector     : UnityEvent<Vector3>    {}
        [Serializable] public class UnityEventGameObject : UnityEvent<GameObject> {}

        // Custom data
        [Serializable] public class UnityEventMeter          : UnityEvent<UIMeterArgs>    {}
        [Serializable] public class UnityEventCollisionState : UnityEvent<CollisionState> {}
        [Serializable] public class UnityEventMovementState  : UnityEvent<MovementState>  {}
        [Serializable] public class UnityEventAirborneState  : UnityEvent<AirborneState>  {}
        [Serializable] public class UnityEventWallState      : UnityEvent<WallState>      {}
    }
}
