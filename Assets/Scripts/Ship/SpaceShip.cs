using SpaceGame.Weapon;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace SpaceGame.Ship
{
    public abstract class SpaceShip : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var laser = collision.gameObject.GetComponent<Laser>();

            if (laser == null)
                return;
            Destroy(laser.gameObject);

            _currentHealth -= laser.GetDamage();

            if (IsLife())
                return;

            Destroy(gameObject);
        }

        private bool IsLife()
        {
            return _currentHealth > 0;
        }
    }
}