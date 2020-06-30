using UnityEngine;

namespace Francisco
{
    public class Item : MonoBehaviour, IInteractable
    {
        public enum ItemType { NONE, CONSUMABLE, WEAPON }

        public string   Name;
        public Sprite   Icon;
        public ItemType Type;

        // no need for properties if you can get/set freely
        //public Sprite   Icon { get => icon;     private set => icon     = value; }
        //public ItemType Type { get => itemType; private set => itemType = value; }

        Inventory inv;

        private void Start()
        {
            inv = Inventory.Instance;
        }

        public void Interact()
        {
            inv.AddItem( this );
        }
    }
}