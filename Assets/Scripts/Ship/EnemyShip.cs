using SpaceGame.SaveSystem;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGame.Ship
{
    public class EnemyShip : SpaceShip
    {
        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private float _firstShootDelay = 3;

        private EnemyShip _enemyShip;

        private PlayerShip _player;
        private PlayerShip[] _players;

        private Vector3 _delta;
        private Quaternion _rotation;

        protected override void OnUpdate()
        {
            if (_player == null)
            {
                _player = FindRandomAlivePlayer(_players);
                return;
            }

            _delta = _player.transform.position - transform.position;
            _delta.Normalize();
        }

        public void SetTargets(PlayerShip[] players)
        {
            _players = players;
            _player = FindRandomAlivePlayer(players);

            StartCoroutine(ShootCoroutine());
        }

        protected override void Movement()
        {
            transform.position = transform.position + _delta * _speed;
            var enemyData = GameContext.CurrentGameData.EnemiesData.First(enemyData => enemyData.Id == Guid);
            enemyData.Positions = new[] { transform.position.x, transform.position.y };
        }

        protected override void HandleTargetRotation()
        {
            if (_player == null)
                return;
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

        protected override bool IsFireReady()
        {
            return base.IsFireReady() && _player != null;
        }

        protected override bool IsMovementReady()
        {
            return _player != null;
        }

        public void SetPositions(float[] positions)
        {
            transform.position = new Vector3(positions[0], positions[1], transform.position.z);
        }

        private PlayerShip FindRandomAlivePlayer(PlayerShip[] players)
        {
            var alivePlayers = players
                .Where(player => player != null)
                .ToArray();
            if (!alivePlayers.Any())
                return null;
            var playerIndex = Random.Range(0, alivePlayers.Length);
            return alivePlayers[playerIndex];
        }

        private IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(_firstShootDelay);
            while (IsMovementReady())
            {
                ShootLaser();
                yield return null;
            }
        }
    }
}