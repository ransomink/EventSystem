using UnityEngine;
using UnityEngine.UI;
using Drivers.CharacterStats;
using System.Collections.Generic;

public enum HitDirection
{
    FRONT, REAR, RIGHT, LEFT
}

public struct DamageInfo
{
    public int damage;
    public EquipmentType equipmentType;

    public DamageInfo( int d, EquipmentType e )
    {
        damage        = d;
        equipmentType = e;
    }
}

public class Character : MonoBehaviour
{
    // Player life
    public int  EngineHealth  = 500;

    // For know if the player have a invincible buff or not. False by default.
    public bool Invincibility = false;

    // Stat declaration
    [Header("Stats")]
    public CharacterStat FrontBumperArmor;
    public CharacterStat RearBumperArmor;
    public CharacterStat LeftFlankArmor;
    public CharacterStat RightFlankArmor;
    public CharacterStat WheelArmor;
    public CharacterStat TiresArmor;

    public CharacterStat MaximumSpeed;
    public CharacterStat AccelerationSpeed;
    public CharacterStat DecelerationSpeed;

    public CharacterStat Maneuverability;

    public CharacterStat Damage;

    public float currentSpeed;

    [Header("Public")]
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    //[Header("Serialize Field")]
    //[SerializeField] CraftingWindow craftingWindow;
    //[SerializeField] StatPanel statPanel;
    //[SerializeField] ItemTooltip itemTooltip;
    [SerializeField] Image draggableItem;
    //[SerializeField] DropItemArea dropItemArea;
    //[SerializeField] QuestionDialog questionDialog;

    private BaseItemSlot dragItemSlot;

    public IEnumerable<StatModifier> StatModifiers { get; internal set; }

    private void OnValidate()
    {
        //if ( itemTooltip == null )
        //{
        //    itemTooltip = FindObjectOfType<ItemTooltip>();
        //}
    }

    private void Awake()
    {
        //statPanel.SetStats( FrontBumperArmor, RearBumperArmor, LeftFlankArmor, RightFlankArmor, WheelArmor, TiresArmor, MaximumSpeed, AccelerationSpeed, DecelerationSpeed, Maneuverability, Damage );
        //statPanel.UpdateStatValues();

        currentSpeed = 0f;

        // Setup Events:
        // Right Click
        inventory.OnRightClickEvent += InventoryRightClick;
        equipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        // Pointer Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        //craftingWindow.OnPointerEnterEvent += ShowTooltip;
        // Pointer Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        //craftingWindow.OnPointerExitEvent += HideTooltip;
        // Begin Drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        // End Drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        // Drag
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        // Drop
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        //dropItemArea.OnDropEvent += DropItemOutsideUI;
    }

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.T ) )
        {
            TakeFrontDamage( 10 );
            //TakeRearDamage ( 10 );
            //TakeRightDamage( 10 );
            //TakeLeftDamage ( 10 );
            Debug.Log( EngineHealth );
        }
    }

    private void InventoryRightClick( BaseItemSlot itemSlot )
    {
        if ( itemSlot.Item is EquippableItem )
        {
            Equip( ( EquippableItem )itemSlot.Item );
        }
        else if ( itemSlot.Item is UsableItem )
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use( this );

            if ( usableItem.IsConsumable )
            {
                inventory.RemoveItem( usableItem );
                usableItem.Destroy();
            }
        }
    }

    private void EquipmentPanelRightClick( BaseItemSlot itemSlot )
    {
        if ( itemSlot.Item is EquippableItem )
        {
            UnEquip( ( EquippableItem )itemSlot.Item );
        }
    }

    private void ShowTooltip( BaseItemSlot itemSlot )
    {
        if ( itemSlot.Item != null )
        {
            //itemTooltip.ShowTooltip( itemSlot.Item );
        }
    }

    private void HideTooltip( BaseItemSlot itemSlot )
    {
        //if ( itemTooltip.gameObject.activeSelf )
        //{
        //    itemTooltip.HideTooltip();
        //}
    }

    private void BeginDrag( BaseItemSlot itemSlot )
    {
        if ( itemSlot.Item != null )
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.gameObject.SetActive( true );
        }
    }

    private void Drag( BaseItemSlot itemSlot )
    {
        draggableItem.transform.position = Input.mousePosition;
    }

    private void EndDrag( BaseItemSlot itemSlot )
    {
        dragItemSlot = null;
        draggableItem.gameObject.SetActive( false );
    }

    private void Drop( BaseItemSlot dropItemSlot )
    {
        if ( dragItemSlot == null )
        {
            return;
        }

        if ( dropItemSlot.CanAddStack( dragItemSlot.Item ) )
        {
            AddStacks( dropItemSlot );
        }
        else if ( dropItemSlot.CanReceiveItem( dragItemSlot.Item ) && dragItemSlot.CanReceiveItem( dropItemSlot.Item ) )
        {
            SwapItems( dropItemSlot );
        }
    }

    private void AddStacks( BaseItemSlot dropItemSlot )
    {
        int numAddablesStacks = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;
        int stacksToAdd = Mathf.Min(numAddablesStacks, dragItemSlot.Amount);

        // Add stacks until drop slot is full
        // Remove the same number of stacks from drag item
        dropItemSlot.Amount += stacksToAdd;
        dragItemSlot.Amount -= stacksToAdd;
    }

    private void SwapItems( BaseItemSlot dropItemSlot )
    {
        EquippableItem dragItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if ( dropItemSlot is EquipmentSlot )
        {
            if ( dropItem != null )
            {
                dropItem.UnEquip( this );
            }
            if ( dragItem != null )
            {
                dragItem.Equip( this );
            }
        }

        if ( dragItemSlot is EquipmentSlot )
        {
            if ( dragItem != null )
            {
                dragItem.UnEquip( this );
            }
            if ( dropItem != null )
            {
                dropItem.Equip( this );
            }
        }

        //statPanel.UpdateStatValues();

        Item draggedItem      = dragItemSlot.Item;
        int draggedItemAmount = dragItemSlot.Amount;

        dragItemSlot.Item   = dropItemSlot.Item;
        dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item   = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    private void DropItemOutsideUI()
    {
        if ( dragItemSlot == null )
        {
            return;
        }

        //questionDialog.Show();
        BaseItemSlot baseItemSlot = dragItemSlot;
        //questionDialog.OnYesEvent += () => DestroyItemInSlot( baseItemSlot );
    }

    private void DestroyItemInSlot( BaseItemSlot baseItemSlot )
    {
        baseItemSlot.Item.Destroy();
        baseItemSlot.Item = null;
    }

    public void Equip( EquippableItem item )
    {
        if ( inventory.RemoveItem( item ) )
        {
            EquippableItem previousItem;
            if ( equipmentPanel.AddItem( item, out previousItem ) )
            {
                if ( previousItem != null )
                {
                    inventory.AddItem( previousItem );
                    previousItem.UnEquip( this );
                    //statPanel.UpdateStatValues();
                }
                item.Equip( this );
                //statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem( item );
            }
        }
    }

    public void UnEquip( EquippableItem item )
    {
        if ( inventory.CanAddItem( item ) && equipmentPanel.RemoveItem( item ) )
        {
            item.UnEquip( this );
            //statPanel.UpdateStatValues();
            inventory.AddItem( item );
        }
    }

    public void UpdateStatValues()
    {
        //statPanel.UpdateStatValues();
    }

    public void TakeDamage( int damage, EquipmentType type )
    {
        // Invincible?
        if ( Invincibility ) return;

        CharacterStat stat = null;

        switch ( type )
        {
            case EquipmentType.FrontArmor:
                stat = FrontBumperArmor;
                break;
            case EquipmentType.RearArmor:
                stat = RearBumperArmor;
                break;
            case EquipmentType.RightArmor:
                stat = RightFlankArmor;
                break;
            case EquipmentType.LeftArmor:
                stat = LeftFlankArmor;
                break;
            default:
                Debug.Log( $"No equipped armor.Taking full damage" );
                break;
        }

        if ( stat != null )
        {
            damage -= ( int )stat.Value;
            damage = Mathf.Clamp( damage, 0, int.MaxValue );
        }

        // Get armor
        var armor = equipmentPanel.GetEquipmentType( type );

        // Armor equipped?
        if ( armor == null )
        {
            EngineHealth -= damage;
            return;
        }

        armor.CurrentArmorDurability -= damage;

        // Armor destroyed?
        if ( armor.CurrentArmorDurability <= 0 )
        {
            EngineHealth -= Mathf.Abs( armor.CurrentArmorDurability );
            armor.CurrentArmorDurability = 0;
        }
    }

    public void TakeDamage( DamageInfo info )
    {
        // Invincible?
        if ( Invincibility ) return;

        CharacterStat stat = null;
        EquipmentType type = EquipmentType.Engine;

        switch ( info.equipmentType )
        {
            case EquipmentType.FrontArmor:
                stat = FrontBumperArmor;
                type = EquipmentType.FrontArmor;
                break;
            case EquipmentType.RearArmor:
                stat = RearBumperArmor;
                type = EquipmentType.RearArmor;
                break;
            case EquipmentType.RightArmor:
                stat = RightFlankArmor;
                type = EquipmentType.RightArmor;
                break;
            case EquipmentType.LeftArmor:
                stat = LeftFlankArmor;
                type = EquipmentType.LeftArmor;
                break;
            default:
                Debug.Log( $"No equipped armor.Taking full damage" );
                break;
        }

        if ( stat != null )
        {
            info.damage -= ( int )stat.Value;
            info.damage  = Mathf.Clamp( info.damage, 0, int.MaxValue );
        }

        // Get armor
        var armor = equipmentPanel.GetEquipmentType( type );

        // Armor equipped?
        if ( armor == null )
        {
            EngineHealth -= info.damage;
            return;
        }

        armor.CurrentArmorDurability -= info.damage;

        // Armor destroyed?
        if ( armor.CurrentArmorDurability <= 0 )
        {
            EngineHealth -= Mathf.Abs( armor.CurrentArmorDurability );
            armor.CurrentArmorDurability = 0;
        }
    }

    public void TakeFrontDamage( int damage )
    {
        // Invincible?
        if ( Invincibility ) return;

        // Reduce damage taken by armor value
        damage -= ( int )FrontBumperArmor.Value;
        damage  = Mathf.Clamp( damage, 0, int.MaxValue );

        // Get armor
        var armor = equipmentPanel.GetEquipmentType(EquipmentType.FrontArmor);
        Debug.Log( $"Front Armor: {armor}" );

        // Armor equipped?
        if ( armor == null )
        {
            EngineHealth -= damage;
            return;
        }

        armor.CurrentArmorDurability -= damage;

        // Armor destroyed?
        if ( armor.CurrentArmorDurability <= 0 )
        {
            EngineHealth -= Mathf.Abs( armor.CurrentArmorDurability );
            armor.CurrentArmorDurability = 0;
        }
    }

    public void TakeWheelDamage( int damage )
    {
        if ( Invincibility ) return;

        damage -= ( int )WheelArmor.Value;
        damage  = Mathf.Clamp( damage, 0, int.MaxValue );

        EngineHealth -= damage;
    }

    public void TakeTiresDamage( int damage )
    {
        if ( Invincibility ) return;

        damage -= ( int )TiresArmor.Value;
        damage  = Mathf.Clamp( damage, 0, int.MaxValue );

        EngineHealth -= damage;
    }

    public void RepairArmor( int index )
    {
        // Repair Kit available?
        Item  repairKit = inventory.GetItem("Item Repair Kit");
        if ( !repairKit )
        {
            Debug.Log( $"An Item Repair Kit is needed to repair armor" );
            return;
        }

        // Get armor
        EquipmentType  type  = ( EquipmentType )index;
        EquippableItem armor = equipmentPanel.GetEquipmentType( type );

        // Armor equipped?
        if ( !armor )
        {
            Debug.Log( $"No {type} is currently equipped. Cannot repair armor" );
            return;
        }

        // Repair?
        if ( !armor.CanRepair ) return;

        armor.Repair();
        inventory.RemoveItem( repairKit.ID ).Destroy();
    }

    public void RepairArmor( string equipmentType )
    {
        // Repair Kit available?
        Item  repairKit = inventory.GetItem("Item Repair Kit");
        if ( !repairKit )
        {
            Debug.Log( $"An Item Repair Kit is needed to repair armor" );
            return;
        }

        // Get armor
        EquipmentType  type  = ( EquipmentType )System.Enum.Parse( typeof( EquipmentType ), equipmentType );
        EquippableItem armor = equipmentPanel.GetEquipmentType( type );

        // Armor equipped?
        if ( !armor )
        {
            Debug.Log( $"No {type} is currently equipped. Cannot repair armor" );
            return;
        }

        // Repair?
        if ( !armor.CanRepair ) return;

        armor.Repair();
        inventory.RemoveItem( repairKit.ID ).Destroy();
    }
}