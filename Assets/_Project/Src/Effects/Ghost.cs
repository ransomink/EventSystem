using Ransomink.Utils;
using UnityEngine;

namespace Ransomink
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] protected Timer    timer;
        [SerializeField] protected Gradient gradient;

        private Color _color;
        private SpriteRenderer _sr;
        private bool  _useGradientFade;

        private void Awake()
        {
            if (!_sr) _sr = GetComponent<SpriteRenderer>();
            if (!_sr) _sr = gameObject.AddComponent<SpriteRenderer>();
            timer = new Timer();
        }

        private void Update()
        {
            if (!timer.IsDone)
            {
                Fade();
            }
            else
            {
                Reset();
            }
        }

        private void Reset()
        {
            gameObject.SetActive(false);
            _sr.sprite = null;
            _sr.color = _color = Color.white;
        }

        public void Init(Sprite s, Color c, Vector3 p, float t = 1f)
        {
            _sr.sprite = s;
            _sr.color  = c;
            _color     = _sr.color;
            transform.position = p;
            gameObject.SetActive(true);
            timer.NewDuration(t + (t / 4));
        }

        public void SetGradient(Gradient g, bool flag)
        {
            gradient = g;
            _useGradientFade = flag;
        }

        private void Fade()
        {
            if (_useGradientFade)
            {
                GradientFade();
            }

            _color.a = Mathf.Lerp(_sr.color.a, 0f, timer.PercentageDoneSmoothStep / 2f);
            _sr.color = _color;
        }

        private void GradientFade()
        {
            _color = gradient.Evaluate(timer.PercentageDone * 2f);
        }
    }
}
