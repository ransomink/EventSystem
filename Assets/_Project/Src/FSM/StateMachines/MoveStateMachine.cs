using UnityEngine;

namespace Ransomink.FSM
{
    public class MoveStateMachine : BaseStateMachine
    {
        [Header("REF")]
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] private Move move;
        [SerializeField] private Dash     dash;

        [Header("STATE")]
        [SerializeField] private MovementState  movement;
        [SerializeField] private AirborneState  airborne;
        [SerializeField] private CollisionState collision;

        [Header("SPEED")]
        [SerializeField] protected float  airSpeed;
        [SerializeField] protected float  runSpeed;
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float tolerance;

        [Header("FORCE")]
        [Range(0f, .1f)]
        [SerializeField] protected float    drag;
        [Range(0f, .1f)]
        [SerializeField] protected float airDrag;
        [SerializeField] protected AnimationCurve dragCurve;

        [Space]
        [Range(0f, 1f)]
        [SerializeField] protected float   accel;
        [Range(0f, 1f)]
        [SerializeField] protected float   decel;
        [SerializeField] protected AnimationCurve accelCurve;
        
        public MovementState  MovementState  { get => movement;  private set => movement  = value; }
        public AirborneState  AirborneState  { get => airborne;  private set => airborne  = value; }
        public CollisionState CollisionState { get => collision; private set => collision = value; }
        public Rigidbody2D Rigidbody => rb2D;
        public Transform Transform => _transform;
        public Move      Move      => move;
        public Dash      Dash      => dash;
        public float     AirSpeed  => airSpeed;
        public float     RunSpeed  => runSpeed;
        public float     MoveSpeed => moveSpeed;
        public float     Tolerance => tolerance;
        public float     Drag      => drag;
        public float     AirDrag   => airDrag;
        public AnimationCurve DragCurve => dragCurve;
        public float     Accel     => accel;
        public float     Decel     => decel;
        public AnimationCurve AccelCurve => accelCurve;

        private void Awake()
        {
            _transform = transform;
            if (!rb2D) rb2D = GetComponent<Rigidbody2D>();
        }

        public override void Start() => TransitionTo(new IdleState());

        public void SetMovementState(MovementState state)   => MovementState  = state;
        public void SetAirborneState(AirborneState state)   => AirborneState  = state;
        public void SetCollisionState(CollisionState state) => CollisionState = state;

        public void OnMoveEvent(Vector3 direction)
        {
            if (ActiveState is IdleState idleState) idleState.OnMoveEvent(direction);
            if (ActiveState is MoveState moveState) moveState.OnMoveEvent(direction);
        }

        private Transform _transform;
    }
}
