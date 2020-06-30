using UnityEngine;

namespace InkFloyd
{
    public class CollisionCheck : MonoBehaviour
    {
        [Header("SETTINGS")]
        public bool OnGround;

        [Header("FIELDS")]
        [SerializeField] private LayerMask mask; // Ground mask
        [SerializeField] private float   radius; // Collision radius

        [Header("OFFSET")]
        [SerializeField] private Vector3 bottom; // Placement of ground check

        [Header("DEBUG")]
        [SerializeField] private Color    debug = Color.magenta; // Visual color of collision in scene

        private Collider[] _colliders; // Colliders hit from physics check. Used for performance
        private Transform  _t;         // Cache transform

        void Awake()
        {
            _t = transform;
        }

        private void Start()
        {
            _colliders = new Collider[ 1 ];
        }

        void Update()
        {
            if ( Physics.OverlapSphereNonAlloc( _t.position + bottom, radius, _colliders, mask ) != 0 )
            {
                OnGround = true;
                Debug.LogWarning( $"Collided With: {_colliders[ 0 ]}" );
            }
            else
            {
                OnGround = false;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = debug;
            Gizmos.DrawWireSphere( transform.position + bottom, radius );
        }
    }
}
