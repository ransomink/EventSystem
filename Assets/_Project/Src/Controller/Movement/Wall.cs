using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class Wall : MonoBehaviour
    {
        [Header("REF")]
        [SerializeField] protected Rigidbody2D rb;

        [Header("STATE")]
        [SerializeField] protected CollisionState cState;
        [SerializeField] protected WallState      wState;

        [Header("FIELDS")]
        [SerializeField] protected float slide;

        [Header("EVENTS")]
        [SerializeField] protected WallEvent OnWall;

        private void Start()
        {
            if (!rb) rb = GetComponent<Rigidbody2D>();
            _curVelocity = Vector2.zero;
        }

        private void Update()
        {
            if (cState != CollisionState.WALL) return;

            _curVelocity = rb.velocity;

            if (Mathf.Abs(_dir.x) > 0f)
            {
                wState = UnityEngine.Input.GetKey(KeyCode.LeftShift) ? WallState.GRAB : WallState.SLIDE;
            }
            else
            {
                wState = WallState.NONE;
            }

            OnWall.Raise(wState);

            switch (wState)
            {
                case WallState.NONE:
                    break;
                case WallState.GRAB:
                    WallGrab();
                    break;
                case WallState.SLIDE:
                    WallSlide();
                    break;
            }
        }

        private void WallGrab()
        {
            //_wallVelocity.Set(_wallVelocity.x, _dir.y * 2f);
            _curVelocity.Set(_curVelocity.x, _curVelocity.y - _curVelocity.y);
            rb.velocity = _curVelocity;
        }

        private void WallSlide()
        {
            _curVelocity.Set(_curVelocity.x, Mathf.Clamp(_curVelocity.y, -slide, float.MaxValue));
            //rb.velocity = _wallVelocity.y > -slide ? _wallVelocity : rb.velocity;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(_curVelocity.y, -slide, float.MaxValue));
        }

        public void OnCollisionEvent(CollisionState state)
        {
            if (cState == state) return;
            cState = state;

            if (cState != CollisionState.LEDGE && cState != CollisionState.WALL) wState = WallState.NONE;
        }

        public void OnInputEvent(Vector3 dir)
        {
            _dir = dir;
        }

        private Vector2 _dir;
        private Vector2 _curVelocity;
    }
}
