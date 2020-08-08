using UnityEngine;

namespace Ransomink
{
    public class ToggleObject : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] private bool show = true;
        [SerializeField] private bool invert;

        [Header("FIELDS")]
        [SerializeField] private float timer;
        [SerializeField] private GameObject @object;

        private bool  _countdown;
        private float _timer = 0f;

        private void Start()
        {
            if (!show) invert = !show;
            if (show && @object.activeInHierarchy) Toggle(!show, invert);
        }

        private void Update()
        {
            if (!_countdown) return;

            _timer -= Time.deltaTime;

            if (_timer <= 0f)
            {
                Toggle(!show, invert);
            }
        }

        private void OnValidate()
        {
            invert = !show;
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                Toggle(show, invert);
            }
        }

        private void Toggle(bool active, bool invert = false)
        {
            if (!invert)
            {
                _timer 	   = active ? timer : 0f;
                _countdown = active;
                @object.SetActive(active);
            }
            else
            {
                _timer 	   = !active ? timer : 0f;
                _countdown = !active;
                @object.SetActive(active);
            }
        }
    }
}
