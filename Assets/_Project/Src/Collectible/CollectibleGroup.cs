using UnityEngine;
using System.Collections.Generic;

namespace Ransomink.Collectible
{
    public class CollectibleGroup : MonoBehaviour
    {
        [SerializeField] private List<Collectible> items;

        public List<Collectible> Items => items;
    }
}
