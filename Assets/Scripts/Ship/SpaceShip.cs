using SpaceGame.Player;
using SpaceGame.Weapon;
using UnityEngine;

namespace SpaceGame.Ship
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class SpaceShip : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private Laser _laser;
        [SerializeField] private float _timeBetweenFires;
        private float _currentHealth;

        //private bool _canShoot = true;
        private AudioSource _audioSource;

        private float _timeNextFire;

        //public float minimumHeight; -6.19
        //public float maximumHeight; 6.19
        //public float minimumWidth; -12.03
        //public float maximumWidth; 12.03

        private void Start()
        {
            _currentHealth = _maxHealth;
            _audioSource = GetComponent<AudioSource>();
            OnStart();
        }

        private void Update()
        {
            _timeNextFire -= Time.deltaTime;

            //if (minimumHeight < transform.position.y
            //    && minimumHeight > transform.position.y)
            //    transform.position = new Vector3(x: 0, y: 0, z: 0);

            //if (maximumWidth < transform.position.x
            //    && maximumWidth > transform.position.x)
            //    transform.position = new Vector3(x: 0, y: 0, z: 0);

            OnUpdate();
        }

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
            Instantiate(_laser, new Vector3(positionX, positionY, 0), transform.rotation);
        }

        private bool IsFireReady()
        {
            return _timeNextFire < 0;
        }

        private bool IsLife()
        {
            return _currentHealth > 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var laser = collision.gameObject.GetComponent<Laser>();
            if (laser == null)
                return;

            Destroy(laser.gameObject);

            ////добавлено ,?
            //if (!_canShoot)
            //{
            //    return;
            //}

            _currentHealth -= laser.GetDamage();

            if (IsLife())
                return;

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _audioSource.Play();
            //пишет ошибку!!!!!!!!
        }

        //public void OnPointerEnter(PointerEventData eventData)
        //{
        //    _canShoot = false;
        //}

        //public void OnPointerExit(PointerEventData eventData)
        //{
        //    _canShoot = true;
        //}
    }
}