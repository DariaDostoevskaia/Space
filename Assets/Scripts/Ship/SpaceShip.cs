using SpaceGame.Audio;
using SpaceGame.Weapon;
using UnityEngine;

namespace SpaceGame.Ship
{
    public abstract class SpaceShip : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private Laser _laser;
        [SerializeField] private float _timeBetweenFires;
        [SerializeField] private AudioClip _destroyAudio;

        private float _currentHealth;
        private float _timeNextFire;

        public float CurrentHealth => _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;

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

            Destroy(laser.gameObject);

            _currentHealth -= laser.GetDamage();

            if (IsLife())
                return;

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            AudioController.Play(_destroyAudio);
        }
    }
}