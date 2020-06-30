using Ransomink.Utils;
using UnityEngine;

namespace Ransomink.Weapons
{
    internal class GunLogic : MonoBehaviour
    {
        [SerializeField] private Gun gunData;
        [SerializeField] private Transform spawnPoint;

        public Transform SpawnPoint => spawnPoint;

        private void Start()
        {
            _gun = gunData as IGun;
            _gun.Init();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && Time.time > _nextFire) Shoot();
            if (UnityEngine.Input.GetKeyDown(KeyCode.R)) _gun.Reload();
            if (UnityEngine.Input.GetMouseButtonDown(1)) _gun.Ads();
        }

        private void Shoot()
        {
            _gun.Shoot();
        }
        
        private IGun  _gun;
        private float _nextFire;
        private Timer _adsTimer;
        private Timer _reloadTimer;
    }
}