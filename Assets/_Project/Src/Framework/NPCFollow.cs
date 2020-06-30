using UnityEngine;

namespace Ransomink
{
    public class NPCFollow : ObjectFollow
    {
        [Header("NPC")]
        [SerializeField] private bool  facingRight;

        protected override void Follow()
        {
            FacingDirection();
            base.Follow();
        }

        protected override void Move()
        {
            var dot = Vector3.Dot(target.right, (Transform.position - target.position).normalized);

            Debug.Log($"Dot Product: {dot}");
            if (dot > 0f) 
            {
                Debug.Log($"Target facing us");
                if (IsDone) return;
            }

            base.Move();
        }

        private void FacingDirection()
        {
            if (Transform.position.x > target.position.x && facingRight)
            {
                Flip();
            }
            else if (Transform.position.x < target.position.x && !facingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            Transform.localEulerAngles += new Vector3(0f, 180f % 360f, 0f);
            facingRight = !facingRight;
        }
    }
}
