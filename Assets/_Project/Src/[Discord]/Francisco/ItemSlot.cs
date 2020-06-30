using UnityEngine;
using UnityEngine.UI;

namespace Francisco
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] Item  item;
        [SerializeField] Image image;

        public Item Item
        {
            get { return item; }
            set
            {
                item = value;
                if ( item == null )
                {
                    image.enabled = false;
                }
                else
                {
                    image.sprite = item.Icon;
                    image.enabled = true;
                }
            }
        }

        public Image Image
        {
            get => image;
            set => image = value;
        }

        private void OnValidate()
        {
            if ( Image == null ) Image = GetComponent<Image>();
        }
    }
}