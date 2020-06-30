using UnityEngine;

public class MoveController : MonoBehaviour
{
    [Header("SETTINGS")]
    [SerializeField] private bool isJumping;

    [Header("REF")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected InkFloyd.CollisionCheck collision;

    [Header("MOVE")]
    [SerializeField] private float speed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float tolerance = .01f;

    [Header("JUMP")]
    [Range(1, 5)]
    [SerializeField] private int   maxJump;
    [SerializeField] private int   curJump;
    [SerializeField] private float jumpForce;

    private float   _hor;
    private Vector3 _dir;

	void Awake()
    {
        rb        = GetComponent<Rigidbody>();
        collision = GetComponent<InkFloyd.CollisionCheck>();
	}

    private void Start()
    {
        _dir = Vector3.zero;
    }

    void Update()
    {
        if ( collision.OnGround ) ResetJump();

        _hor = Input.GetAxis( "Horizontal" );
        if ( Mathf.Abs( _hor ) >= 0f + tolerance )
        {
            Move();
        }

        if ( Input.GetButtonDown( "Jump" ) ) isJumping = true;

    }

    private void FixedUpdate()
    {
        if ( isJumping ) Jump();
    }

    private void Move()
    {
        speed = collision.OnGround ? groundSpeed : airSpeed;
        _dir.Set( _hor * speed, rb.velocity.y, rb.velocity.z );
        rb.velocity = _dir;
    }

    private void Jump()
    {
        if ( curJump < maxJump )
        {
            rb.velocity += transform.up * jumpForce;
            //
            collision.OnGround = false;
            curJump++;
        }
    }

    private void ResetJump()
    {
        curJump   = 0;
        isJumping = false;
    }
}
