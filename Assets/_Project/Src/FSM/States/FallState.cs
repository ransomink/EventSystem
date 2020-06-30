using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEngine;

namespace Ransomink.FSM
{
    public class FallState : BaseState
    {
        public override void OnEnter(IStateMachine stateMachine)
        {
            Owner = stateMachine;
            _subOwner = Owner as JumpStateMachine;
            Init();
        }

        public override void OnUpdate()
        {
            DoFall();
        }

        private void Init()
        {
            _rb2D      = _subOwner.Rigidbody;
            _transform = _subOwner.transform;
            _fallMult  = _subOwner.Jump.FallMultiplier;
            _airborneState = _subOwner.AirborneState;
        }

        private void DoFall()
        {
            if (_airborneState != AirborneState.FALL)
            {
                _subOwner.SetAirborneState(AirborneState.FALL);
            }

            _curVelocity  += (Vector2)_transform.up * (Physics2D.gravity.y * (_fallMult - 1f) * Time.deltaTime);
            _rb2D.velocity = _curVelocity;
        }

        public void OnCollisionEvent()
        {
            _collisionState = _subOwner.CollisionState;
            if (_collisionState == CollisionState.GROUND) Owner.TransitionTo(new GroundState());
        }

        private Void _void;
        private float _fallMult;
        private Rigidbody2D _rb2D;
        private Transform _transform;
        private Vector2 _curVelocity;
        private JumpStateMachine _subOwner;
        private AirborneState  _airborneState;
        private CollisionState _collisionState;
    }
}
