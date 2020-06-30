//using Drivers.LocalizationSettings;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] Text Name;
    [SerializeField] Text Desc;
    [SerializeField] Text Type;
    [SerializeField] Text Durability;

    private string CheckItemNameLanguage( string name )
    {
        //name = name.Replace( "Engine repair kit", LocalizationManager.Instance.GetText( "ENGINE_REPAIR_KIT" ) );
        //name = name.Replace( "Invincibility",     LocalizationManager.Instance.GetText( "INVINCIBILITY"     ) );
        //name = name.Replace( "Scrap coin",        LocalizationManager.Instance.GetText( "SCRAP_COIN"        ) );
        //name = name.Replace( "Engine",            LocalizationManager.Instance.GetText( "ENGINE"            ) );
        //name = name.Replace( "Front bumper",      LocalizationManager.Instance.GetText( "FRONT_BUMPER"      ) );
        //name = name.Replace( "Rear bumper",       LocalizationManager.Instance.GetText( "REAR_BUMPER"       ) );
        //name = name.Replace( "Left protection",   LocalizationManager.Instance.GetText( "LEFT_PROTECTION"   ) );
        //name = name.Replace( "Right protection",  LocalizationManager.Instance.GetText( "RIGHT_PROTECTION"  ) );
        //name = name.Replace( "Steering wheel",    LocalizationManager.Instance.GetText( "STEERING_WHEEL"    ) );
        //name = name.Replace( "Tires",             LocalizationManager.Instance.GetText( "TIRES"             ) );

        return name;
    }

    private string CheckItemDescLanguage( string desc )
    {
        //desc = desc.Replace( "Front armor",     LocalizationManager.Instance.GetText( "FRONT_ARMOR"     ) );
        //desc = desc.Replace( "Rear armor",      LocalizationManager.Instance.GetText( "REAR_ARMOR"      ) );
        //desc = desc.Replace( "Left armor",      LocalizationManager.Instance.GetText( "LEFT_ARMOR"      ) );
        //desc = desc.Replace( "Right armor",     LocalizationManager.Instance.GetText( "RIGHT_ARMOR"     ) );
        //desc = desc.Replace( "Tires armor",     LocalizationManager.Instance.GetText( "TIRES_ARMOR"     ) );
        //desc = desc.Replace( "Wheel armor",     LocalizationManager.Instance.GetText( "WHEEL_ARMOR"     ) );
        //desc = desc.Replace( "Max. speed",      LocalizationManager.Instance.GetText( "MAX_SPEED"       ) );
        //desc = desc.Replace( "Acceleration",    LocalizationManager.Instance.GetText( "ACCELERATION"    ) );
        //desc = desc.Replace( "Deceleration",    LocalizationManager.Instance.GetText( "DECELERATION"    ) );
        //desc = desc.Replace( "Maneuverability", LocalizationManager.Instance.GetText( "MANEUVERABILITY" ) );
        //desc = desc.Replace( "Damage",          LocalizationManager.Instance.GetText( "DAMAGE"          ) );

        return desc;
    }

    private string CheckItemTypeLanguage( string type )
    {
        //type = type.Replace( "Engine",     LocalizationManager.Instance.GetText( "ENGINE"           ) );
        //type = type.Replace( "FrontArmor", LocalizationManager.Instance.GetText( "FRONT_BUMPER"     ) );
        //type = type.Replace( "RearArmor",  LocalizationManager.Instance.GetText( "REAR_BUMPER"      ) );
        //type = type.Replace( "LeftArmor",  LocalizationManager.Instance.GetText( "LEFT_PROTECTION"  ) );
        //type = type.Replace( "RightArmor", LocalizationManager.Instance.GetText( "RIGHT_PROTECTION" ) );
        //type = type.Replace( "Wheel",      LocalizationManager.Instance.GetText( "STEERING_WHEEL"   ) );
        //type = type.Replace( "Tires",      LocalizationManager.Instance.GetText( "TIRES"            ) );
        //type = type.Replace( "Consumable", LocalizationManager.Instance.GetText( "CONSUMABLE"       ) );
        //type = type.Replace( "Usable",     LocalizationManager.Instance.GetText( "USABLE"           ) );

        return type;
    }

    /*private string CheckItemDurabilityLanguage( string durability )
    {
        durability = durability.Replace("Durability", LocalizationManager.Instance.GetText("DURABILITY"));

        return durability;
    }*/

    public void ShowTooltip( Item item )
    {
        string name = item.ItemName;
        name        = CheckItemNameLanguage( name );
        Name.text   = name;

        string type = item.GetItemType();
        type        = CheckItemTypeLanguage( type );
        Type.text   = type;

        //if (item.GetItemType() == "FrontArmor")
        //{
        //    Durability.text = "Durability " + equipmentDurability.CurrentFrontArmorDurability + " / " + equipmentDurability.FrontArmorDurability;
        //    Durability.text = CheckItemDurabilityLanguage(Durability.text);
        //}
        //else if (item.GetItemType() == "RearArmor")
        //{
        //    Durability.text = "Durability " + equipmentDurability.CurrentRearArmorDurability + " / " + equipmentDurability.RearArmorDurability;
        //    Durability.text = CheckItemDurabilityLanguage(Durability.text);
        //}
        //else if(item.GetItemType() == "LeftArmor")
        //{
        //    Durability.text = "Durability " + equipmentDurability.CurrentLeftArmorDurability + " / " + equipmentDurability.LeftArmorDurability;
        //    Durability.text = CheckItemDurabilityLanguage(Durability.text);
        //}
        //else if(item.GetItemType() == "RightArmor")
        //{
        //    Durability.text = "Durability " + equipmentDurability.CurrentRightArmorDurability + " / " + equipmentDurability.RightArmorDurability;
        //    Durability.text = CheckItemDurabilityLanguage(Durability.text);
        //}

        string desc = item.GetDescription();
        desc        = CheckItemDescLanguage( desc );
        Desc.text   = desc;

        gameObject.SetActive( true );
    }

    public void HideTooltip()
    {
        gameObject.SetActive( false );
    }
}