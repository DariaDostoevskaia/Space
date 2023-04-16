using SpaceGame.Player;
using SpaceGame.Ship;
using SpaceGame.Weapon;
using UnityEngine;

namespace SpaceGame.Enemy
{
    public class EnemyShip : SpaceShip
    {
        [SerializeField] private float _speed = 1.5f;
        private MousePlayerShip _player;
        private Vector3 _delta;

        protected override void OnStart()
        {
            _player = FindObjectOfType<MousePlayerShip>();
        }

        private void Update()
        {
            _delta = _player.transform.position - transform.position;
            _delta.Normalize();
        }

        private void FixedUpdate()
        {
            transform.position = transform.position + _delta * _speed;
        }
    }
}