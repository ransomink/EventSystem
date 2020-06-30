using UnityEngine;

public class HUDText : MonoBehaviour
{
    private PlayerInventory PInventory;

    [SerializeField] private int HUDVariable;   //1 - render weapon 1 ammo, 2 - render weapon 2 ammo, 3 - render current HP, 4 - render current SP, 9 - render current framerate
    [SerializeField] private TextMesh HUDValue;

    private void Awake()
    {
        PInventory = GameObject.FindWithTag( "Player" ).GetComponent<PlayerInventory>();

        if ( !PInventory )
        {
            Debug.LogError( "HUD object error: Cannot find player game object, will attempt to create 'link' to game object at next available opportunity" );
        }
    }

    private void Update()
    {
        if ( !PInventory ) PInventory = GameObject.FindGameObjectWithTag( "Player" ).GetComponent<PlayerInventory>();

        switch ( HUDVariable )
        {
            case 1:
                if ( PInventory.GetWeapons().Length == 0 )
                {
                    HUDValue.text = null;
                }
                else
                {
                    HUDValue.text = PInventory.GetWeaponAmmo( 1 ).ToString();
                }
                break;

            case 2:
                HUDValue.text = PInventory.RAmmo.ToString();
                break;

            case 3:
                HUDValue.text = PInventory.Player_HP.ToString();
                if ( PInventory.Player_HP > 65 )
                {
                    HUDValue.color = Color.green;
                }
                else if ( PInventory.Player_HP > 25 )
                {
                    HUDValue.color = Color.yellow;
                }
                else
                {
                    HUDValue.color = Color.red;
                }
                break;

            case 4:
                HUDValue.text = PInventory.Player_SP.ToString();
                if ( PInventory.Player_SP < 25 )
                {
                    HUDValue.color = Color.red;
                }
                else
                {
                    HUDValue.color = Color.white;
                }
                break;

            case 9:
                float FPS = 1.0f / Time.deltaTime;
                HUDValue.text = FPS.ToString();
                if ( FPS > 30 )
                {
                    HUDValue.color = Color.green;
                }
                else
                {
                    HUDValue.color = Color.yellow;
                }
                break;

            default:
                print( "No HUD setting specified. Text field will not be updated" );
                break;
        }
    }
}
