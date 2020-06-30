using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class JumpCommand : BaseCommand
    {
        public JumpCommand(Void v, Event e)
        {
            _void  = v;
            OnJump = e;
        }

        public override void Execute()
        {
            Jump();
        }

        private void Jump()
        {
            Debug.Log($"Pressed Jump: {OnJump.name}");
            OnJump?.Raise(_void);
        }

        private Event OnJump;
        private readonly Void _void;
    }
}
