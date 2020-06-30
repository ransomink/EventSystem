using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class RunCommand : BaseCommand
    {
        public RunCommand(bool b, BoolEvent e)
        {
            _flag = b;
            OnRun = e;
        }

        public override void Execute()
        {
            Run();
        }

        private void Run()
        {
            Debug.Log($"Pressed Run");
            OnRun?.Raise(_flag);
        }

        private BoolEvent OnRun;
        private bool _flag;
    }
}
