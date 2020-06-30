using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //GameObject containers
                     public  int Player_MaxHP;     // Max Health
                     public  int Player_MaxSP;     // Max Stamina
    [SerializeField] private int Player_MaxEP;     // Max Energy
    public  int Player_HP;                         // Current Health
    public  int Player_SP;                         // Current Stamina
    private int Player_EP;                         // Current Energy

    //Equipment variables
    [SerializeField] private int Max_Grenades = 3; // Maximum Grenades
                     public  int Grenades;         // Current Grenades
                     public  int PAmmo;
                     public  int RAmmo;
    [SerializeField] private GameObject[] ProjectileWeapons = new GameObject[2];
    [SerializeField] private GameObject[] Backpack          = new GameObject[30];

    void Start()
    {
        Grenades  = Max_Grenades;
        Player_HP = Player_MaxHP;
        Player_SP = Player_MaxSP;
        Player_EP = Player_MaxEP;
    }

    public void AddWeapon( GameObject weaponToAdd )
    {
        // Cycle through equipped weapons
        for ( int i = 0; i < ProjectileWeapons.Length; i++ )
        {
            if ( !ProjectileWeapons[ i ] )
            {
                ProjectileWeapons[ i ] = Instantiate( weaponToAdd, transform.position, transform.rotation, transform );
                return;
            }
        }

        AddToInventory( weaponToAdd );
    }

    public void AddToInventory( GameObject itemToAdd )
    {
        // Cycle through stored items
        for ( int i = 0; i < Backpack.Length; i++ )
        {
            if ( !Backpack[ i ] )
            {
                Backpack[ i ] = itemToAdd;
                return;
            }
        }

        Debug.LogWarning( $"Inventory full - cannot add item {itemToAdd}" );
    }

    private void UseItem( GameObject itemToUse )
    {
        if ( itemToUse.CompareTag( "Consumable" ) )
        {
            Instantiate( itemToUse, transform.position, transform.rotation );
        }
    }

    public GameObject[] GetWeapons()
    {
        if ( ProjectileWeapons.Length > 0 )
        {
            return ProjectileWeapons;
        }

        return null;
    }

    public int GetWeaponAmmo( int wpnNumber )
    {
        if ( ProjectileWeapons[ wpnNumber ] )
        {
            return ProjectileWeapons[wpnNumber].GetComponent<ProjectileWeapon>().GetAmmoData();
        }

        return 0;
    }

    public int[] GetAmmoData()
    {
        return new int[] { PAmmo, RAmmo };
    }

    public void UpdateAmmoData( int[] AmmoPouch )
    {
        PAmmo = AmmoPouch[ 0 ];
        RAmmo = AmmoPouch[ 1 ];
    }
}

public class ProjectileWeapon : MonoBehaviour
{
    protected internal int GetAmmoData()
    {
        throw new NotImplementedException();
    }
}
