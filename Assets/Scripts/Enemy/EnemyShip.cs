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
            //FindRandomPlayers

            StartCoroutine(ShootCoroutine());
        }

        //private PlayerShip FindRandomPlayer()
        //{
        //    var playerIndex = Random.Range(0, 2);

        //    switch (playerIndex)
        //    {
        //        case 0:
        //            return _firstPlayer != null
        //                        ? _firstPlayer
        //                        : _secondPlayer;

        //        case 1:
        //            return _secondPlayer != null
        //                        ? _secondPlayer
        //                        : _firstPlayer;

        //        default: throw new System.Exception();
        //    }
        //}

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