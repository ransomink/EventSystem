using UnityEngine;

namespace Ransomink.FSM
{
    public class GroundState : BaseState
    {
        public override void OnEnter(IStateMachine stateMachine)
        {
            Owner = stateMachine;
            _subOwner = Owner as JumpStateMachine;
            Init();
        }

        private void Init()
        {
            _airborneState  = AirborneState.NONE;
            _collisionState = CollisionState.GROUND;
            _subOwner.SetAirborneState(_airborneState);
            _subOwner.SetCollisionState(_collisionState);
        }

        public void OnJumpEvent() => Owner.TransitionTo(new JumpState());

        private JumpStateMachine _subOwner;
        private AirborneState  _airborneState;
        private CollisionState _collisionState;
    }
}
