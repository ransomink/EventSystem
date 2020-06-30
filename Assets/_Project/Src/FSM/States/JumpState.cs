using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEngine;

namespace Ransomink.FSM
{
    public class JumpState : BaseState
    {
        public override void OnEnter(IStateMachine stateMachine)
        {
            Owner = stateMachine;
            _subOwner = Owner as JumpStateMachine;
            Init();
            DoJump();
        }

        public override void OnUpdate()
        {
            _curVelocity = _rb2D.velocity;

            if (_curVelocity.y < 0f && _collisionState == CollisionState.AIR)
            {
                Owner.TransitionTo(new FallState());
            }
            else if (_curVelocity.y > 0f && _airborneState != AirborneState.JUMP)
            {
                DoLowJump();
            }
        }

        private void Init()
        {
            _rb2D      = _subOwner.Rigidbody;
            _transform = _subOwner.transform;
            _force     = _subOwner.Jump.Force;
            _jumpMax   = _subOwner.Jump.JumpMax;
            _jumpMult  = _subOwner.Jump.JumpMultiplier;
            _fallMult  = _subOwner.Jump.FallMultiplier;
            _collisionState = _subOwner.CollisionState;
        }

        public void DoJump()
        {
            // Double jump?
            if (_collisionState == CollisionState.AIR && _jumpNum >= _jumpMax) return;

            _subOwner.SetAirborneState(AirborneState.JUMP);
            _subOwner.SetCollisionState(CollisionState.AIR);
            _jumpDirection  = _transform.up;
            _curVelocity   += _jumpDirection * _force;
            _rb2D.velocity  = _curVelocity; 
            Debug.LogWarning($"Jump Force: {_curVelocity}");
            _jumpNum++;
        }

        private void DoLowJump()
        {
            _curVelocity += (Vector2)_transform.up * (Physics2D.gravity.y * (_jumpMult - 1f) * Time.deltaTime);
            _rb2D.velocity   = _curVelocity;
        }

        public void OnFallEvent() => Owner.TransitionTo(new FallState());
        public void OnJumpEvent() => DoJump();

        private Void     _void;
        private Event   OnFall;
        private float   _force;
        private float _jumpNum;
        private float _jumpMax;
        private float _fallMult;
        private float _jumpMult;
        private Transform _transform;
        private Rigidbody2D _rb2D;
        private Vector2  _jumpDirection;
        private Vector2    _curVelocity;
        private JumpStateMachine _subOwner;
        private AirborneState  _airborneState;
        private CollisionState _collisionState;
    }
}
