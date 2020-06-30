using System.Collections.Generic;
using UnityEngine;

namespace Ransomink
{
    public class Invoker : MonoBehaviour
    {
        static Queue<ICommand> buffer;

        private void Awake()
        {
            buffer = new Queue<ICommand>();
        }

        public static void AddCommand(ICommand command)
        {
            buffer.Enqueue(command);
        }

        private void Update()
        {
            if (buffer.Count == 0) return;
            
            buffer.Dequeue().Execute();
        }
    }
}
