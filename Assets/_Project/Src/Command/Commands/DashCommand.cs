using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class DashCommand : BaseCommand
    {
        public DashCommand(Void v, Event e)
        {
            _void  = v;
            OnDash = e;
        }

        public override void Execute()
        {
            Dash();
        }

        private void Dash()
        {
            Debug.Log($"Pressed Dash");
            OnDash?.Raise(_void);
        }

        private Event OnDash;
        private readonly Void _void;
    }
}
