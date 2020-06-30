using UnityEngine;

namespace Ransomink.FSM
{
    public abstract class BaseStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField] private bool   isSubFsm;

        public bool   IsSubFsm => isSubFsm;
        public IState ActiveState { get => _activeState; protected set => value = _activeState; }

        public virtual void Start() => TransitionTo(null);

        public void FixedUpdate()
        {
            if (_activeState == null) return;
            _activeState.OnFixedUpdate();
        }

        public void Update()
        {
            if (_activeState == null) return;
            _activeState.OnUpdate();
        }

        public void LateUpdate()
        {
            if (_activeState == null) return;
            _activeState.OnLateUpdate();
        }

        public void TransitionTo(IState state)
        {
            _activeState = state;
            _activeState.OnEnter(this);
        }
        
        private IState _activeState;
    }
}
