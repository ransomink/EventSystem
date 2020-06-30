using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class MoveCommand : BaseCommand
    {
        public MoveCommand(Vector2 d, VectorEvent e)
        {
            _direction = d;
            OnInput    = e;
        }

        public override void Execute()
        {
            Move();
        }

        private void Move()
        {
            Debug.Log($"Pressed Move");
            OnInput?.Raise(_direction);
        }

        private VectorEvent OnInput;
        private Vector2  _direction;
    }
}
