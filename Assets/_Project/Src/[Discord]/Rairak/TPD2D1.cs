using UnityEngine;

public class TPD2D1 : MonoBehaviour
{
    private GameObject Player;
    private GameObject exitDoor;
    private Collider2D playerCollider;
    private bool isTouching = false;
    public float maxDistance = 1;
    public bool canUse;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag( "Player" );
        exitDoor = GameObject.FindGameObjectWithTag( "D1" );
        canUse = true;
        playerCollider = Player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance( Player.transform.position, this.transform.position ) < maxDistance )
        {
            isTouching = true; // they are touching AND close
        }
        else
        {
            isTouching = false;
        }
        if ( isTouching == true )
        {
            OnColliderEnter2D( playerCollider );
        }
    }

    void OnColliderEnter2D( Collider2D playerCollider )
    {
        if ( Input.GetButtonDown( "Vertical" ) && playerCollider.gameObject.tag == "Player" )
        {
            Player.transform.position = exitDoor.transform.position;
            canUse = false;
            if ( Input.GetButtonUp( "Vertical" ) )
            {
                canUse = true;
            }
            print( "D2D1 is called" );
        }
    }
}