using Ransomink.Events;
using Ransomink.Utils;
using UnityEngine;

namespace Ransomink
{
    public class Dash : MonoBehaviour
    {
        [Header("REF")]
        [SerializeField] protected Rigidbody2D rb;

        [Header("STATE")]
        [SerializeField] protected CollisionState cState;
        [SerializeField] protected bool isDashing;

        [Header("FIELDS")]
        [SerializeField] protected Vector2 _dir;
        [Space()]
        [SerializeField] protected float  speed;
        [SerializeField] protected float length;
        [SerializeField] protected AnimationCurve speedCurve;

        [Header("EVENTS")]
        [SerializeField] protected FloatEvent OnDashStart;
        [SerializeField] protected Ransomink.Events.Event OnDashEnd;

        private Transform   _t;
        private Timer   _timer;
        private Vector2  _dash;
        private Vector2  _smoothVel;
        private readonly Void _void;

        private void Start()
        {
            _t            = transform;
            _timer        = new Timer();
            _dash         = Vector2.zero;
            _smoothVel    = Vector2.zero;
            if (!rb) rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!isDashing) return;
            DoDash();
            //if (_timer.IsDone)
            //{
            //    OnDashEnd.Raise(_void);
            //    isDashing = false;
            //}
        }

        public void DoDash()
        {
            if (cState != CollisionState.AIR) _dir.Set(_dir.x, 0f);

            _smoothVel  = Vector2.SmoothDamp(rb.velocity, _dir * (speed * speedCurve.Evaluate(_timer.PercentageDone)), ref _smoothVel, Time.deltaTime);
            rb.velocity = _smoothVel;

            if (_timer.IsDone)
            {
                isDashing = false;
                OnDashEnd.Raise(_void);
            }
        }

        public void OnCollisionEvent(CollisionState state)
        {
            if (cState == state) return;
            cState = state;
        }

        public void OnInputEvent(Vector3 dir)
        {
            _dir = dir;
        }

        public void OnDashEvent(Void v)
        {
            //if (isDashing || _dir == Vector2.zero) return;
            //if (cState != CollisionState.AIR) _dir.Set(_dir.x, 0f);

            isDashing = true;
            //OnDashStart.Raise(speed);
            //Debug.Log($"Dashing...");
            _timer.NewDuration(length);
        }
    }
}
