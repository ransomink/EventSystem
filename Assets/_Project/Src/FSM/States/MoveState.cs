using Ransomink.Extensions;
using Ransomink.Utils;
using UnityEngine;

namespace Ransomink.FSM
{
    public class MoveState : BaseState
    {
        public MoveState(Vector3 direction) => _dir = direction;

        private float GetMoveDeltaSquared
        {
            get
            {
                return (_prevPos.x - _t.position.x) * (_prevPos.x - _t.position.x)
                     + (_prevPos.y - _t.position.y) * (_prevPos.y - _t.position.y);
            }
        }
        public override void OnEnter(IStateMachine stateMachine)
        {
                Owner = stateMachine;
            _subOwner = stateMachine as MoveStateMachine;
            Init();
        }

        public override void OnUpdate()
        {
            Move();
        }

        private void Init()
        {
            _t    = _subOwner.transform;
            _rb2D = _subOwner.Rigidbody;
            _movementState  = _subOwner.MovementState;
            _airborneState  = _subOwner.AirborneState;
            _collisionState = _subOwner.CollisionState;
            _airSpeed       = _subOwner.AirSpeed;
            _runSpeed       = _subOwner.RunSpeed;
            _moveSpeed      = _subOwner.MoveSpeed;
            _tolerance      = _subOwner.Tolerance;
            _drag           = _subOwner.Drag;
            _airDrag        = _subOwner.AirDrag;
            _dragCurve      = _subOwner.DragCurve;
            _accel          = _subOwner.Accel;
            _decel          = _subOwner.Decel;
            _accelCurve     = _subOwner.AccelCurve;
            _timer          = new Timer();
        }

        private void Move()
        {
            var curVelocity = _rb2D.velocity;
            _move = new Vector2(_dir.x, curVelocity.y);

            // Moved?
            if (_move == Vector2.zero && GetMoveDeltaSquared <= _tolerance.Squared())
            {
                Owner.TransitionTo(new IdleState());
            }
            else
            {
                switch (_collisionState)
                {
                    case CollisionState.AIR:
                        _targetDrag  = _airDrag;
                        if (_targetSpeed > _runSpeed) break;
                        _targetSpeed = _airSpeed;
                        break;
                    case CollisionState.GROUND:
                        _targetDrag = _drag;
                        if (_targetSpeed >= _runSpeed) break;
                        _targetSpeed = _moveSpeed;
                        break;
                    case CollisionState.WALL:
                    case CollisionState.LEDGE:
                        _targetDrag = _drag;
                        if (_targetSpeed < _moveSpeed) _targetSpeed = _moveSpeed;
                        break;
                    case CollisionState.CEILING:
                        _targetSpeed = _airSpeed;
                        break;
                    default:
                        _targetSpeed = _moveSpeed;
                        _targetDrag  = _drag;
                        _curAccel    = _accel;
                        break;
                }

                _subOwner.SetMovementState(MovementState.WALK);
                
                Debug.Log($"Prev Speed: {_prevSpeed} | Target Speed: {_targetSpeed}");

                if (!_transition && _prevSpeed != _targetSpeed)
                {
                    _transition = true;
                    _curAccel   = _prevSpeed < _targetSpeed ? _accel : _decel;
                    _timer.NewDuration(_curAccel);
                    Debug.Log("Transitioning...");
                }

                if (_transition)
                {
                    _curDrag    = Mathf.Lerp(_prevDrag, _targetDrag, _dragCurve.Evaluate(_timer.PercentageDone));
                    _curSpeed   = Mathf.Lerp(_prevSpeed, _targetSpeed, _accelCurve.Evaluate(_timer.PercentageDone));
                    _transition = !_timer.IsDone;
                    Debug.Log($"Current Speed: {_curSpeed}");
                }

                _move.Set(_move.x * _curSpeed, _move.y);
                _smoothVel  = Vector2.SmoothDamp(curVelocity, _move, ref _smoothVel, _curDrag);
                _rb2D.velocity = _smoothVel;
            }

            // Get previous states
            _prevPos = _t.position;
            if (!_transition) 
            {
                _prevDrag  = _curDrag;
                _prevSpeed = _targetSpeed;
            }
        }

        public void OnMoveEvent(Vector3 direction) => _dir = direction;

        private Rigidbody2D      _rb2D;
        private MoveStateMachine _subOwner;
        private MovementState    _movementState;
        private AirborneState    _airborneState;
        private CollisionState   _collisionState;

        private float  _airSpeed;
        private float  _runSpeed;
        private float _moveSpeed;
        private float _tolerance;

        private float    _drag;
        private float _airDrag;
        private AnimationCurve _dragCurve;

        private float   _accel;
        private float   _decel;
        private AnimationCurve _accelCurve;

        private Transform _t;
        private Vector2   _dir;
        private Vector2   _move;
        private Vector2   _prevPos;
        private Vector2   _smoothVel;
        private float     _curSpeed;
        private float     _prevSpeed;
        private float     _targetSpeed;
        private float     _curDrag;
        private float     _prevDrag;
        private float     _targetDrag;
        private float     _curAccel;
        private Timer     _timer;
        private bool      _transition;
    }
}
