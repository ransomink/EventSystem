using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class Collision : MonoBehaviour
    {
        [Header("FIELDS")]
        [SerializeField] private LayerMask       mask;
        [SerializeField] private CollisionState state;
        [SerializeField] private float radius = 0.25f;

        [Header("OFFSET")]
        [SerializeField] private Vector3 top;
        [SerializeField] private Vector3 left;
        [SerializeField] private Vector3 right;
        [SerializeField] private Vector3 bottom;

        [Header("EVENTS")]
        [SerializeField] private CollisionEvent OnCollision;

        [Header("DEBUG")]
        [SerializeField] private Color debug = Color.magenta;

        private void Awake()
        {
            _t = transform;
        }

        private void Update()
        {
            if (Physics2D.OverlapCircle(_t.position + top, radius, mask))
            {
                state = CollisionState.CEILING;
            }
            else if (Physics2D.OverlapCircle(_t.position + left, radius, mask) ||
                 Physics2D.OverlapCircle(_t.position + right, radius, mask))
            {
                state = CollisionState.WALL;
            }
            else if (Physics2D.OverlapCircle(_t.position + bottom, radius, mask))
            {
                state = CollisionState.GROUND;
            }
            else
            {
                state = CollisionState.AIR;
            }

            UpdateCollision();
        }

        private void UpdateCollision()
        {
            if (_cState == state) return;
            OnCollision?.Raise(state);
            _cState = state;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = debug;

            Gizmos.DrawWireSphere(transform.position + top, radius);
            Gizmos.DrawWireSphere(transform.position + left, radius);
            Gizmos.DrawWireSphere(transform.position + right, radius);
            Gizmos.DrawWireSphere(transform.position + bottom, radius);
        }

        private Transform      _t;
        private CollisionState _cState;
    }
}
