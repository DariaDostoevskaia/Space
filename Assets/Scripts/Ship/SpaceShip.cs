using SpaceGame.Audio;
using SpaceGame.Weapon;
using System;
using UnityEngine;

namespace SpaceGame.Ship
{
    public abstract class SpaceShip : MonoBehaviour
    {
        public event Action OnEnemyDestroyed;

        [SerializeField] private float _maxHealth;
        [SerializeField] private Laser _laser;
        [SerializeField] private float _timeBetweenFires;
        [SerializeField] private AudioClip _destroyAudio;
        [SerializeField] private GameObject _explosion;

        private float _currentHealth;
        private float _timeNextFire;

        public float CurrentHealth => _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        private void Start()
        {
            OnStart();
        }

        private void Update()
        {
            _timeNextFire -= Time.deltaTime;

            HandleTargetRotation();
            OnUpdate();
        }

        private void FixedUpdate()
        {
            if (!IsMovementReady())
                return;
            Rotation();
            Movement();
        }

        protected abstract void Movement();

        protected abstract void Rotation();

        protected abstract void HandleTargetRotation();

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected void ShootLaser()
        {
            if (!IsFireReady())
                return;

            _timeNextFire = _timeBetweenFires;

            float positionX = transform.position.x;
            float positionY = transform.position.y;

            var laser = Instantiate(_laser, new Vector3(positionX, positionY, 0), transform.rotation);

            laser.SetOwner(gameObject.tag);

            laser.OnEnemyDestroyed += DestroyEnemy;

            void DestroyEnemy()
            {
                OnEnemyDestroyed?.Invoke();
                laser.OnEnemyDestroyed -= DestroyEnemy;
            }
        }

        protected virtual bool IsFireReady()
        {
            return _timeNextFire < 0;
        }

        protected virtual bool IsMovementReady()
        {
            return true;
        }

        private bool IsLife()
        {
            return _currentHealth > 0;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var laser = collision.gameObject.GetComponent<Laser>();
            if (laser == null)
                return;

            if (gameObject.CompareTag(laser.OwnerTag))
                return;

            _currentHealth -= laser.GetDamage();

            if (IsLife())
            {
                Destroy(laser.gameObject);
                return;
            }

            laser.DestroyEnemy();
            Destroy(laser.gameObject);
            Destroy(gameObject);
            Instantiate(_explosion, gameObject.transform.position, Quaternion.identity);
        }

        private void OnDestroy()
        {
            AudioController.Play(_destroyAudio);
        }
    }
}