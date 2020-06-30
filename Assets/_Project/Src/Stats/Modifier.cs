using UnityEngine;

namespace Ransomink.Stats
{
    [System.Serializable]
    public class Modifier
    {
        [SerializeField] private ModType type;
        [SerializeField] private float  value;
    }
}
