using Ransomink.Utils;
using UnityEngine;

namespace Ransomink
{
    public class GhostEffect2D : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] protected bool useGhost = true;
        [SerializeField] protected bool useGradient;
        [SerializeField] protected bool useGradientFade;

        [Header("FIELDS")]
        [Range(0f, .5f)]
        [SerializeField] protected float tick = .05f;
        [SerializeField] protected float length = 1f;
        [SerializeField] protected int   count  =  8;
        [SerializeField] protected Ghost ghost;
        [SerializeField] protected GameObject parent;
        [SerializeField] protected new SpriteRenderer renderer;
        [SerializeField] protected Gradient gradient;

        [Header("GHOSTS")]
        [SerializeField] protected Ghost[] ghosts;

        private int       _index;
        private Transform _tForm;
        private Timer     _timer;
        private Color     _color;
        private Vector3 _prevPos;

        private void Awake()
        {
            _index = 0;
            _tForm = transform;
            _timer = new Timer();
            ghosts = new Ghost[count];
            if (!renderer) renderer = GetComponent<SpriteRenderer>();
            _color = renderer.color;
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            // Ghost?
            if (useGhost) DoTrail();
        }

        private void Init()
        {
            // Create container?
            if (!parent)
            {
                parent = new GameObject("GhostEffect");
            }

            // Setup ghost
            for (var i = 0; i < ghosts.Length; ++i)
            {
                var ghost = new GameObject($"Ghost{i}").AddComponent<Ghost>();
                ghost.transform.SetParent(parent.transform);
                ghost.gameObject.SetActive(false);
                ghosts[i] = ghost;
            }

            _prevPos = _tForm.position;
            _timer.NewDuration(tick);
        }

        private void DoTrail()
        {
            // Moved?
            if (_prevPos == _tForm.position) return;

            // Poll?
            if (!_timer.IsDone)
            {
                _prevPos = _tForm.position;
                return;
            }

            // Reset trail?
            _index = _index >= ghosts.Length ? 0 : _index;

            // Color from sprite or gradient?
            _color = useGradient ? gradient.Evaluate(_index / (float)count) : renderer.color;

            // Fade sprite from gradient?
            if (useGradientFade) ghosts[_index].SetGradient(gradient, useGradientFade);

            // Init ghost
            ghosts[_index].Init(renderer.sprite, _color, _tForm.position, length);
            _prevPos = _tForm.position;
            _timer.NewDuration(tick);
            _index++;
        }

        public void OnDashEvent()
        {
            useGhost = true;
        }

        public void OnDashEndEvent()
        {
            useGhost = false;
        }
    }
}
