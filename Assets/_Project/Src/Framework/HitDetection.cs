using UnityEngine;

namespace Ransomink.Utils
{
    public class HitDetection : MonoBehaviour
    {
        public enum Direction
        {
            NONE = 0, NORTH = 1, EAST = 2, SOUTH = 3, WEST = 4,
            NORTHEAST = 5,  SOUTHEAST = 6, SOUTHWEST = 7, NORTHWEST = 8
        }

        [Header("SETTINGS")]
        [SerializeField] private bool useLogger;
        [SerializeField] private bool useOrdinal;

        [Header("FIELDS")]
        // Used for testing in Update
        //[SerializeField] private Transform target;
        [SerializeField] private Direction direction;

        private void Start() => _t = transform;

        // Used for testing without needing contact
        /* private void Update()
        {
            HitDirection(target.position);
        } */

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (_t) HitDirection(other.GetContact(0).point);
        }

        private void HitDirection(Vector3 position)
        {
            var dir = position - _t.position;
            var dot = Vector3.Dot(dir.normalized, _t.forward);

            Log($"Dot Product: {dot}");

            var dotAbs = Mathf.Abs(dot); 
            if (dotAbs > .7f)
            {
                Log($"Object is Parallel!");
                direction = CardinalDirection(position, true);
            }
            else if (useOrdinal && dotAbs > .4f)
            {
                Log($"Object is Diagonal!");
                direction = OrdinalDirection(dot, position);
            }
            else
            {
                Log($"Object is Perpendicular!");
                direction = CardinalDirection(position, false);
            }
        }

        private Direction CardinalDirection(Vector3 position, bool parallel)
        {
            var relativePoint = _t.InverseTransformPoint(position);
            if (parallel)
            {
                if (relativePoint.z > 0f) 
                {
                    Log($"Collided From the Front");
                    return Direction.NORTH;
                }
                else 
                {
                    Log($"Collided From Behind");
                    return Direction.SOUTH;
                }
            }
            else
            {
                if (relativePoint.x > 0f) 
                {
                    Log($"Collided from Right");
                    return Direction.EAST;
                }
                else 
                {
                    Log($"Collided from Left");
                    return Direction.WEST;
                }
            }
        } 

        private Direction OrdinalDirection(float dot, Vector3 position)
        {
            var north         = dot > 0f ? true : false;
            var relativePoint = _t.InverseTransformPoint(position);

            if (north)
            {
                if (relativePoint.x > 0f) 
                {
                    Log($"collided From Top-Right");
                    return Direction.NORTHEAST;
                }
                else 
                {
                    Log($"collided From Top-Left");
                    return Direction.NORTHWEST;
                }
            }
            else
            {
                if (relativePoint.x > 0f) 
                {
                    Log($"collided From Bottom-Right");
                    return Direction.SOUTHEAST;
                }
                else 
                {
                    Log($"collided From Bottom-Left");
                    return Direction.SOUTHWEST;
                }
            }
        }

        private void Log(string text)
        {
            if (useLogger) Debug.Log(text);
        }

        private Transform _t;
    }
}
