using UnityEngine;

namespace Ransomink.FSM
{
    public class JumpStateMachine : BaseStateMachine
    {
        [Header("REF")]
        [SerializeField] private Rigidbody2D rb2D;
        [SerializeField] private Jump        jump;

        [Header("STATE")]
        [SerializeField] private AirborneState  airborne;
        [SerializeField] private CollisionState collision;

        public Rigidbody2D    Rigidbody     => rb2D;
        public Jump           Jump          => jump;
        public Transform      Transform     => _transform;
        public AirborneState  AirborneState  { get => airborne;  private set => airborne  = value; }
        public CollisionState CollisionState { get => collision; private set => collision = value; }

        private void Awake()
        {
            _transform = transform;
            if (!rb2D) rb2D = GetComponent<Rigidbody2D>();
        }

        public override void Start() => TransitionTo(new GroundState());

        public void SetAirborneState(AirborneState state)   => AirborneState  = state;
        public void SetCollisionState(CollisionState state) => CollisionState = state;

        public void OnJumpEvent()
        {
            if (ActiveState is JumpState jumpState)     jumpState.OnJumpEvent();
            if (ActiveState is GroundState groundState) groundState.OnJumpEvent();
        }

        public void OnCollisionEvent()
        {
            if (ActiveState is FallState fallState) fallState.OnCollisionEvent();
        }

        public void OnFallEvent()
        {
            if (ActiveState is JumpState jumpState) jumpState.OnFallEvent();
        }

        private Transform _transform;
    }
}
