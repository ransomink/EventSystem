using UnityEngine;

namespace Ransomink.Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool isActive = false;
        private static T   _instance;
        public  static T    Instance
        {
            get
            {
                if (isActive)
                {
                    Debug.LogWarning($"Singleton Instance {typeof(T)} is already destroyed. Returning null.");
                    return null;
                }
                
                if (!_instance)
                {
                    _instance = FindObjectOfType<T>();
                    if (!_instance) _instance = CreateInstance();
                }

                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (!_instance)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private static T CreateInstance()
        {
            var go       = new GameObject();
            var instance = go.AddComponent<T>();
            go.name      = $"{typeof(T).ToString()} Controller";
            DontDestroyOnLoad(go);
            return instance;
        }

        private void OnApplicationQuit()
        {
            isActive = true;
        }

        private void OnDestroy()
        {
            isActive = true;
        }
    }
}
