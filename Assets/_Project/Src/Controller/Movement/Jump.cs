using Event = Ransomink.Events.Event;
using Ransomink.Events;
using Ransomink.Utils;
using UnityEngine;

namespace Ransomink
{
    public class Jump : MonoBehaviour
    {
        [Header("REF")]
        [SerializeField] protected Rigidbody2D rb2D;

        [Header("STATE")]
        [SerializeField] protected CollisionState cState;
        [SerializeField] protected MovementState  mState;
        [SerializeField] protected AirborneState  aState;
        [SerializeField] protected WallState      wState;
        [SerializeField] protected bool    isWallJumping;

        [Header("FIELDS")]
        [SerializeField] protected float    force =  5f;
        [SerializeField] protected float fallMult =  1f;
        [SerializeField] protected float jumpMult =  2f;
        [SerializeField] protected float jumpTime = .1f;

        [Range(0, 5)]
        [SerializeField] protected int  jumpNum;
        [Range(0, 5)]
        [SerializeField] protected int  jumpMax = 2;

        [Header("EVENTS")]
        [SerializeField] protected Event OnJump;
        [SerializeField] protected Event OnJumpWall;
        [SerializeField] protected Event OnJumpWallEnd;
        [SerializeField] protected Event OnFall;

        public Rigidbody2D Rigidbody => rb2D;
        public float Force           => force;
        public float FallMultiplier  => fallMult;
        public float JumpMultiplier  => jumpMult;
        public float JumpTime        => jumpTime;
        public float JumpNum         => jumpNum;
        public float JumpMax         => jumpMax;
        public Event OnFallEvent     => OnFall;

        private void Awake()
        {
            if (!rb2D) rb2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _timer = new Timer();
            _jumpDirection = Vector2.zero;
        }

        private void Update()
        {
            _curVelocity = rb2D.velocity;
            if (_curVelocity.y < 0f && cState == CollisionState.AIR)
            {
                DoFall();
            }
            else if (_curVelocity.y > 0f && aState != AirborneState.JUMP)
            {
                DoLowJump();
            }

            //if (isWallJumping && _timer.IsDone)
            //{
            //    isWallJumping = false;
            //    OnJumpWallEnd.Raise(_void);
            //}

            //if (UnityEngine.Input.GetButtonDown("Jump")) DoJump();
        }

        public void DoJump()
        {
            // Double jump?
            if (cState == CollisionState.AIR && jumpNum >= jumpMax) return;

            _jumpDirection = transform.up;

            // Wall jump?
            //if (cState == CollisionState.WALL)
            //{
            //    _jumpDir.Set(-_dir.x, _jumpDir.y);
            //    OnJumpWall.Raise(_void);
            //    Debug.LogWarning($"Wall Jump");
            //    Debug.LogWarning($"Jump Direction: {_jumpDir}");
            //}

            //ResetVelocity();
            //OnJump.Raise(_void);
            aState = AirborneState.JUMP;
            _curVelocity += _jumpDirection * force;
            rb2D.velocity = _curVelocity; 
            Debug.LogWarning($"Jump Force: {_curVelocity}");
            jumpNum++;
        }

        private void DoFall()
        {
            if (aState != AirborneState.FALL)
            {
                aState = AirborneState.FALL;
                OnFall.Raise(_void);
            }

            _curVelocity += (Vector2)transform.up * (Physics2D.gravity.y * (fallMult - 1f) * Time.deltaTime);
            rb2D.velocity   = _curVelocity;
        }

        private void DoLowJump()
        {
            _curVelocity += (Vector2)transform.up * (Physics2D.gravity.y * (jumpMult - 1f) * Time.deltaTime);
            rb2D.velocity   = _curVelocity;
        }

        public void ResetJump()
        {
            jumpNum = 0;
            aState  = AirborneState.NONE;
        }

        public void ResetVelocity()
        {
            rb2D.velocity = new Vector2(_curVelocity.x, 0f);
        }

        public void OnCollisionEvent(CollisionState state)
        {
            if (cState == state) return;
            cState = state;

            // On surface?
            switch (cState)
            {
                case CollisionState.AIR:
                    break;
                case CollisionState.GROUND:
                case CollisionState.WALL:
                case CollisionState.LEDGE:
                    ResetJump();
                    break;
                case CollisionState.CEILING:
                    ResetVelocity();
                    break;
            }

            //Debug.Log($"Collision State: {cState}");
        }

        public void OnInputEvent(Vector3 dir)
        {
            _direction = dir;
        }

        public void OnMovementEvent(MovementState state)
        {
            if (mState == state) return;
            mState = state;
            //Debug.Log($"Movement State: {mState}");
        }

        public void OnJumpEndEvent()
        {
            if (cState == CollisionState.AIR) aState = AirborneState.FREE;
        }

        public void OnWallEvent(WallState state)
        {
            if (wState == state) return;
            wState = state;
            //Debug.Log($"Wall State: {wState}");
        }

        public void OnWallJumpEvent(Void v)
        {
            isWallJumping = true;
            _timer.NewDuration(jumpTime);
        }

        private Vector3 RandomPointOnCircleEdge(Vector3 center, float radius)
        {
            var pos = Random.insideUnitCircle.normalized * radius;
            return center + new Vector3(pos.x, 0f, pos.y);
        }

        private Vector3 RandomPointOnCircleEdge(Vector3 center, float min, float max)
        {
            var pos = Vector3.zero;
            pos.Set(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            var range = Random.Range(min, max);
            pos = pos.normalized * range;
            return center + pos;
        }

        private Timer   _timer;
        private Vector2 _direction;
        private Vector2 _jumpDirection;
        private Vector2 _curVelocity;
        private readonly Void  _void;
    }
}
