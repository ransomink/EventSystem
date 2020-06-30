using UnityEngine;

namespace Ransomink.FSM
{
    public abstract class BaseState : IState
    {
        public bool         Active { get => _isActive; protected set => _isActive = value; }
        public IStateMachine Owner { get => _owner;    protected set => _owner    = value; }

        public virtual void OnCollisionEnter(Collision collision)     {}
        public virtual void OnCollisionEnter2D(Collision2D collision) {}
        public virtual void OnCollisionExit(Collision collision)      {}
        public virtual void OnCollisionExit2D(Collision2D collision)  {}
        public virtual void OnCollisionStay(Collision collision)      {}
        public virtual void OnCollisionStay2D(Collision2D collision)  {}
        public virtual void OnEnter(IStateMachine stateMachine)       {}
        public virtual void OnExit() {}
        public virtual void OnFixedUpdate() {}
        public virtual void OnLateUpdate()  {}
        public virtual void OnTriggerEnter(Collider other)     {}
        public virtual void OnTriggerEnter2D(Collider2D other) {}
        public virtual void OnTriggerExit(Collider other)      {}
        public virtual void OnTriggerExit2D(Collider2D other)  {}
        public virtual void OnTriggerStay(Collider other)      {}
        public virtual void OnTriggerStay2D(Collider2D other)  {}
        public virtual void OnUpdate() {}

        private bool       _isActive;
        private IStateMachine _owner;
    }
}
