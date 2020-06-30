using UnityEngine;

namespace Ransomink
{
    public class FieldOfView : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float angle;

        private Transform _transform;

        private void Awake() => _transform = transform;

        private void Update()
        {
            var heading = target.position - _transform.position;
            var dot     = Vector3.Dot(heading.normalized, _transform.forward);

            Debug.Log($"Dot Product: {dot}");
            if (dot > 0f) 
            {
                Debug.Log($"Enemy in front!");
            }
            else
            {
                Debug.Log($"Enemy is behind!");
            }

            var angle = Vector3.Angle(heading, _transform.forward);

            Debug.Log($"Angle: {angle}");

            if (angle <= this.angle)
            {
                Debug.Log($"Enemy within viewing angle");
            }
            else
            {
                Debug.Log($"Enemy outside of view angle");
            }
        }
    }  
}
