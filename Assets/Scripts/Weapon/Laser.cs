using System;
using UnityEngine;

namespace SpaceGame.Weapon
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _damage = 1f;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.up * _speed);
        }

        public float GetDamage()
        {
            return _damage;
        }
    }
}