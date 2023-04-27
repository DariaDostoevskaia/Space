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
        private Rigidbody2D _rigidbody;
        private Vector3 _delta;
        private bool _IsRightDirection;

        protected override void OnUpdate()
        {
            //if (_player == null)
            //{
            //    return;
            //}

            _delta = _player.transform.position - transform.position;
            _delta.Normalize();
        }

        private void FixedUpdate()
        {
            transform.position = transform.position + _delta * _speed;
            //if (_IsRightDirection)
            //{
            //    _rigidbody.velocity = Vector2.right * _speed;
            //}
            //else
            //{
            //    _rigidbody.velocity = Vector2.left * _speed;
            //}
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