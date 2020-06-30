using UnityEngine;

namespace Ransomink.FSM
{
    public class IdleState : BaseState
    {
        public override void OnEnter(IStateMachine stateMachine)
        {
            Owner = stateMachine;
        }

        //public override void OnUpdate()
        //{
        //    if (_direction != Vector2.zero) Owner.TransitionTo(new MoveState());
        //}

        public void OnMoveEvent(Vector3 direction) => Owner.TransitionTo(new MoveState(direction));

        //private Vector2 _direction;
    }
}
