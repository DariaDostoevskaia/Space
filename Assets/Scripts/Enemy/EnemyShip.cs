using SpaceGame.Player;
using SpaceGame.Ship;
using System.Collections;
using UnityEngine;

namespace SpaceGame.Enemy
{
    public class EnemyShip : SpaceShip
    {
        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private float _firstShootDelay = 3;
        private PlayerShip _player;
        private Vector3 _delta;

        protected override void OnUpdate()
        {
            if (_player == null)
            {
                return;
            }
            _delta = _player.transform.position - transform.position;
            _delta.Normalize();
        }

        private void FixedUpdate()
        {
            transform.position = transform.position + _delta * _speed;
        }

        public void SetTarget(PlayerShip playerShip)
        {
            _player = playerShip;
            StartCoroutine(ShootCoroutine());
        }

        private IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(_firstShootDelay);
            while (true)
            {
                ShootLaser();
                yield return null;
            }
        }
    }
}