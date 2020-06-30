using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    private void Start()
    {
        for ( int i = 0; i < equipmentSlots.Length; i++ )
        {
            equipmentSlots[ i ].OnPointerEnterEvent += equipSlot => { OnPointerEnterEvent?.Invoke( equipSlot ); };
            equipmentSlots[ i ].OnPointerExitEvent  += equipSlot => { OnPointerExitEvent?.Invoke ( equipSlot ); };
            equipmentSlots[ i ].OnRightClickEvent   += equipSlot => { OnRightClickEvent?.Invoke  ( equipSlot ); };
            equipmentSlots[ i ].OnBeginDragEvent    += equipSlot => { OnBeginDragEvent?.Invoke   ( equipSlot ); };
            equipmentSlots[ i ].OnEndDragEvent      += equipSlot => { OnEndDragEvent?.Invoke     ( equipSlot ); };
            equipmentSlots[ i ].OnDragEvent         += equipSlot => { OnDragEvent?.Invoke        ( equipSlot ); };
            equipmentSlots[ i ].OnDropEvent         += equipSlot => { OnDropEvent?.Invoke        ( equipSlot ); };
        }
    }

    private void OnValidate()
    {
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem( EquippableItem item, out EquippableItem previousItem )
    {
        for ( int i = 0; i < equipmentSlots.Length; i++ )
        {
            if ( equipmentSlots[ i ].EquipmentType == item.EquipmentType )
            {
                previousItem = ( EquippableItem )equipmentSlots[ i ].Item;
                equipmentSlots[ i ].Item = item;
                equipmentSlots[ i ].Amount = 1;
                return true;
            }
        }

        previousItem = null;
        return false;
    }

    public bool RemoveItem( EquippableItem item )
    {
        for ( int i = 0; i < equipmentSlots.Length; i++ )
        {
            if ( equipmentSlots[ i ].Item == item )
            {
                equipmentSlots[ i ].Item = null;
                equipmentSlots[ i ].Amount = 0;
                return true;
            }
        }

        return false;
    }

    public EquippableItem GetEquipmentType( EquipmentType type )
    {
        EquipmentSlot equipSlot = Array.Find( equipmentSlots, slot => slot.EquipmentType == type );
        return equipSlot.Item as EquippableItem;
    }
}