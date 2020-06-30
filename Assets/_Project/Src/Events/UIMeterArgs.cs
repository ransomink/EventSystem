using System;
using UnityEngine;

namespace Ransomink.Events
{
    [Serializable]
    public class UIMeterArgs : EventArgs
    {
        public bool flag;
        public float value;
        public GameObject sender;

        public UIMeterArgs() {}

        public UIMeterArgs(bool f, float v, GameObject s)
        {
            flag   = f;
            value  = v;
            sender = s;
        }
    }
}
