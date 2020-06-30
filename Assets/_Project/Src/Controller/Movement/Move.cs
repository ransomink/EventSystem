using Ransomink.Events;
using Ransomink.Extensions;
using Ransomink.Utils;
using UnityEngine;

namespace Ransomink
{
    public class Move : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] protected bool canMove = true;

        [Header("REF")]
        [SerializeField] protected Rigidbody2D rb;

        [Header("STATE")]
        [SerializeField] protected CollisionState cState;
        [SerializeField] protected MovementState  mState;
        [SerializeField] protected AirborneState  aState;

        [Header("SPEED")]
        [SerializeField] protected float  airSpeed;
        [SerializeField] protected float  runSpeed;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float tolerance;

        [Header("FORCE")]
        [Range(0f, .1f)]
        [SerializeField] protected float    drag;
        [Range(0f, .1f)]
        [SerializeField] protected float airDrag;
        [SerializeField] protected AnimationCurve dragCurve;

        [Space]
        [Range(0f, 1f)]
        [SerializeField] protected float   accel;
        [Range(0f, 1f)]
        [SerializeField] protected float   decel;
        [SerializeField] protected AnimationCurve accelCurve;

        [Header("EVENTS")]
        [SerializeField] protected MovementEvent OnMove;

        protected float GetMoveDeltaSquared
        {
            get
            {
                return (_prevPos.x - _t.position.x) * (_prevPos.x - _t.position.x)
                     + (_prevPos.y - _t.position.y) * (_prevPos.y - _t.position.y);
            }
        }

        private void Awake()
        {
            if (!rb) rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _t       = transform;
            _dir     = Vector2.zero;
            _move    = Vector2.zero;
            _prevPos = Vector2.zero;
            _timer   = new Timer();
        }

        private void Update()
        {
            if (canMove) DoWalk();
        }

        private void DoWalk()
        {
            var curVelocity = rb.velocity;
            _move = new Vector2(_dir.x, curVelocity.y);

            // Moved?
            if (_move == Vector2.zero && GetMoveDeltaSquared <= tolerance.Squared())
            {
                mState = MovementState.IDLE;
            }
            else
            {
                switch (cState)
                {
                    case CollisionState.AIR:
                        _targetDrag  = airDrag;
                        if (_targetSpeed > runSpeed) break;
                        _targetSpeed = airSpeed;
                        break;
                    case CollisionState.GROUND:
                        _targetDrag = drag;
                        if (_targetSpeed >= runSpeed) break;
                        _targetSpeed = moveSpeed;
                        break;
                    case CollisionState.WALL:
                    case CollisionState.LEDGE:
                        _targetDrag = drag;
                        if (_targetSpeed < moveSpeed) _targetSpeed = moveSpeed;
                        break;
                    case CollisionState.CEILING:
                        _targetSpeed = airSpeed;
                        break;
                    default:
                        _targetSpeed = moveSpeed;
                        _targetDrag  = drag;
                        _curAccel    = accel;
                        break;
                }

                mState = MovementState.WALK;
                
                Debug.Log($"Prev Speed: {_prevSpeed} | Target Speed: {_targetSpeed}");

                if (!_transition && _prevSpeed != _targetSpeed)
                {
                    _transition = true;
                    _curAccel   = _prevSpeed < _targetSpeed ? accel : decel;
                    _timer.NewDuration(_curAccel);
                    Debug.Log("Transitioning...");
                }

                if (_transition)
                {
                    _curDrag    = Mathf.Lerp(_prevDrag, _targetDrag, dragCurve.Evaluate(_timer.PercentageDone));
                    _curSpeed   = Mathf.Lerp(_prevSpeed, _targetSpeed, accelCurve.Evaluate(_timer.PercentageDone));
                    _transition = !_timer.IsDone;
                    Debug.Log($"Current Speed: {_curSpeed}");
                }
                //else
                //{
                //    _curDrag = _targetDrag;
                //    curSpeed = _targetSpeed;
                //}

                _move.Set(_move.x * _curSpeed, _move.y);
                _smoothVel  = Vector2.SmoothDamp(curVelocity, _move, ref _smoothVel, _curDrag);
                rb.velocity = _smoothVel;
            }

            OnMove?.Raise(mState);

            // Get previous states
            _prevPos = _t.position;
            if (!_transition) 
            {
                _prevDrag  = _curDrag;
                _prevSpeed = _targetSpeed;
            }
        }

        // Comcast New Self Department: 1-877-405-2298

        //void AirControl()
        //{
        //    float xInput   = Input.GetAxis("Horizontal");
        //    float tarSpeed = xInput * Speed;
        //    float airAcc   = AirControlSpeed * Time.deltaTime;
        //    float tarX     = Mathf.MoveTowards(MoveDir.x, tarSpeed, airAcc);

        //    bool dif = Mathf.Sign(xInput) != Mathf.Sign(MoveDir.x);
        //    bool areZeroes = MoveDir.x == 0 || xInput == 0;
        //    bool isSpeedUP = Mathf.Abs(tarX) > Mathf.Abs(MoveDir.x);

        //    if (areZeroes || dif || isSpeedUP) { MoveDir.x = tarX; }
        //}

        public void OnCollisionEvent(CollisionState state)
        {
            if (cState == state) return;
            cState = state;
        }

        public void OnInputEvent(Vector3 dir)
        {
            _dir = dir;
        }

        public void OnMoveCancel()
        {
            canMove = false;
            Debug.Log($"Cancel Movement");
        }

        public void OnMoveResume()
        {
            canMove = true;
            Debug.Log($"Resume Movement");
        }

        public void OnRunEvent(bool flag)
        {
            if (flag && _targetSpeed < runSpeed) _targetSpeed = runSpeed;
            else if (!flag && _targetSpeed <= runSpeed) _targetSpeed = moveSpeed;
        }

        public void OnWallJumpEvent(float time)
        {
            _timer.NewDuration(time);
        }

        public void OnDashStartEvent(float dash)
        {
            _targetSpeed = dash;
        }

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
