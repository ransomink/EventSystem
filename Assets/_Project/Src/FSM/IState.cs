using UnityEngine;

namespace Ransomink.FSM
{
    public interface IState
    {
        bool         Active { get; }
        IStateMachine Owner { get; }
        void OnCollisionEnter(Collision collision);
        void OnCollisionEnter2D(Collision2D collision);
        void OnCollisionExit(Collision collision);
        void OnCollisionExit2D(Collision2D collision);
        void OnCollisionStay(Collision collision);
        void OnCollisionStay2D(Collision2D collision);
        void OnEnter(IStateMachine stateMachine);
        void OnExit();
        void OnFixedUpdate();
        void OnLateUpdate();
        void OnTriggerEnter(Collider other);
        void OnTriggerEnter2D(Collider2D other);
        void OnTriggerExit(Collider other);
        void OnTriggerExit2D(Collider2D other);
        void OnTriggerStay(Collider other);
        void OnTriggerStay2D(Collider2D other);
        void OnUpdate();
    }
}
