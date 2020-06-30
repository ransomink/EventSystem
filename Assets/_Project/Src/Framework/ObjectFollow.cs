using UnityEngine;

namespace Ransomink
{
    public class ObjectFollow : MonoBehaviour
    {
        [Header("REFERENCE")]
        [SerializeField] protected Transform target;

        [Header("SETTINGS")]
        [SerializeField] protected bool   canFollow;
        [SerializeField] protected bool isFollowing;

        [Header("FIELDS")]
        [SerializeField] protected float      angle;
        [SerializeField] protected float      speed;
        [SerializeField] protected float   distance;

        protected Transform Transform => _t;
        protected Vector3 NewPosition { get => _newPos; set => _newPos = value; }
        protected Vector3 Heading => target.position - Transform.position;
        protected bool    IsDone  => Heading.sqrMagnitude <= distance * distance;

        private void Start()
        {
            _t = transform;
        }

        protected virtual void LateUpdate()
        {
            if (!canFollow) return;
            Follow();
        }

        protected virtual void Follow()
        {
            if (IsDone && isFollowing)
            {
                isFollowing = false; 
                return;
            }
            
            _endPos = target.position;
            _newPos = _endPos + Quaternion.AngleAxis(angle, Vector3.up) * ((-target.forward) * distance);
            Debug.Log($"New Position: {_newPos}");
            isFollowing = true;
            Move();
        }

        protected virtual void Move()
        {
            Vector3 pos = _t.position;
            _t.localPosition = Vector3.MoveTowards(pos, _newPos, speed * Time.deltaTime);

            //if (_t.position == _newPos) isFollowing = false;
        }

        private Transform _t;
        private Vector3   _newPos;
        private Vector3   _endPos;
    }
}
