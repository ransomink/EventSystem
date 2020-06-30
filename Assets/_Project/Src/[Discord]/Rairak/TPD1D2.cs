using UnityEngine;

public class TPD1D2 : MonoBehaviour
{
    [SerializeField] private bool canUse;               // Can we use this door?
    [SerializeField] private bool isTouching = false;   // Is the player touching the door?
    [SerializeField] private GameObject exitDoor;       // Exit door to teleport toward

    private GameObject Player;                          // Self-explanatory

    void Start()
    {
        // Find the player
        Player = GameObject.FindGameObjectWithTag( "Player" );

        // Allow use of door at startup
        if ( !canUse ) canUse = true;
    }

    void Update()
    {
        // Can we teleport?
        if ( Input.GetButtonDown( "Vertical" ) && isTouching && canUse )
        {
            Teleport();
        }

        // Allow door usage
        if ( Input.GetButtonUp( "Vertical" ) )
        {
            canUse = true;
        }
    }

    void OnTriggerEnter2D( Collider2D collider )
    {
        // Player collided?
        if ( collider.CompareTag( "Player" ) )
        {
            // We are in touch
            isTouching = true;
        }
    }

    private void OnTriggerExit2D( Collider2D collider )
    {
        // Player left collider?
        if ( collider.CompareTag( "Player" ) )
        {
            // We're no longer together, rip...
            isTouching = false;
        }
    }

    // Guess what I do?
    void Teleport()
    {
        canUse = false;
        Player.transform.position = exitDoor.transform.position;
    }
}
