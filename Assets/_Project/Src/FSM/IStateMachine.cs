namespace Ransomink.FSM
{
    public interface IStateMachine
    {
        bool   IsSubFsm    { get; }
        IState ActiveState { get; }

        void FixedUpdate();
        void LateUpdate();
        void Start();
        void TransitionTo(IState state);
        void Update();
    }
}
