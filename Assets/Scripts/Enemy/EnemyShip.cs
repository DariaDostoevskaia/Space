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
        private PlayerShip[] _players;
        private Vector3 _delta;
        private Quaternion _rotation;

        protected override void OnUpdate()
        {
            _delta = _player.transform.position - transform.position;
            _delta.Normalize();
        }

        protected override void Movement()
        {
            transform.position = transform.position + _delta * _speed;
        }

        protected override void HandleTargetRotation()
        {
            var playerPosition = _player.transform.position;
            var shipPosition = transform.position;
            var dx = shipPosition.x - playerPosition.x;
            var dy = shipPosition.y - playerPosition.y;
            var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
            var euler = new Vector3(0, 0, angle + 90);
            _rotation = Quaternion.Euler(euler);
        }

        protected override void Rotation()
        {
            transform.rotation = _rotation;
        }

        public void SetTargets(PlayerShip[] players)
        {
            _players = players;

            if (players == null)
                return;
            //for (int i = 0; i < 2; i++)
            //{
            //    transform.LookAt(players);

            //    if (Vector3.Distance(transform.position, players.position) >= MinDist)
            //    {
            //        transform.position += _speed * Time.deltaTime * transform.forward;

            //        if (Vector3.Distance(transform.position, players.transform.position) <= MaxDist)
            //        {
            //            // Put what do you want to happen here
            //        }
            //    }
            //}

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