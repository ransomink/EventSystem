using Event = Ransomink.Events.Event;
using Ransomink.Events;
using UnityEngine;

namespace Ransomink
{
    public class Input : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] private bool useInput;
        [SerializeField] private bool useJump;
        [SerializeField] private bool useDash;
        [SerializeField] private bool useRun;

        [Header("FIELDS")]
        [SerializeField] private float hor;
        [SerializeField] private float ver;

        [Header("EVENTS")]
        [SerializeField] private VectorEvent OnInput;
        [SerializeField] private Event       OnJump;
        [SerializeField] private Event       OnJumpEnd;
        [SerializeField] private Event       OnDash;
        [SerializeField] private BoolEvent   OnRun;

        private void Start()
        {
            _newDir = Vector2.zero;
            _oldDir = Vector2.zero;
        }

        void Update()
        {
            if (useInput)
            {
                hor = UnityEngine.Input.GetAxisRaw("Horizontal");
                ver = UnityEngine.Input.GetAxisRaw( "Vertical" );
                _newDir.Set(hor, ver);

                //if (_newDir != _oldDir) OnInput.Raise(_newDir);
                if (_newDir != _oldDir) Invoker.AddCommand(keyMove = new MoveCommand(_newDir, OnInput));

                _oldDir = _newDir;
            }

            if (useJump && UnityEngine.Input.GetButtonDown("Jump")) Invoker.AddCommand(keyJump = new JumpCommand(_void, OnJump));
            if (useJump && UnityEngine.Input.GetButtonUp  ("Jump")) Invoker.AddCommand(keyJump = new JumpCommand(_void, OnJumpEnd));
            if (useDash && UnityEngine.Input.GetButtonDown("Dash")) Invoker.AddCommand(keyDash = new DashCommand(_void, OnDash));
            if (useRun  && UnityEngine.Input.GetButton    ("Run" )) Invoker.AddCommand(keyRun  = new RunCommand ( true, OnRun));
            if (useRun  && UnityEngine.Input.GetButtonUp  ("Run" )) Invoker.AddCommand(keyRun  = new RunCommand (false, OnRun));
        }

        private ICommand keyMove;
        private ICommand keyJump;
        private ICommand keyDash;
        private ICommand keyRun;

        private Vector2 _newDir;
        private Vector2 _oldDir;
        private readonly Void _void;
    }
}
