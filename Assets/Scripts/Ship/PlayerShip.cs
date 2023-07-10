using SpaceGame.SaveSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace SpaceGame.Ship
{
    public abstract class PlayerShip : SpaceShip
    {
        [SerializeField] private float _playerSpeed = 2f;
        [SerializeField] private float _inertia = 0.9f;
        [SerializeField] private bool _movementBowShip;

        [SerializeField] private List<KeyCode> _upButtons;
        [SerializeField] private List<KeyCode> _downButtons;
        [SerializeField] private List<KeyCode> _leftButtons;
        [SerializeField] private List<KeyCode> _rightButtons;

        [SerializeField] private List<KeyCode> _shootButtons;

        [SerializeField] private float _minimumHeight = -6.19f;
        [SerializeField] private float _maximumHeight = 6.19f;
        [SerializeField] private float _minimumWidth = -12.03f;
        [SerializeField] private float _maximumWidth = 12.03f;

        private float _currentSpeed;
        private Vector3 _lastMovement;
        private Vector3 _movement;
        private Space _movementRelative;

        protected override void OnStart()
        {
            _movementRelative = _movementBowShip
                ? Space.Self
                : Space.World;
        }

        protected override void OnUpdate()
        {
            _movement += GetDirection(_upButtons, Vector3.up);
            _movement += GetDirection(_downButtons, Vector3.down);
            _movement += GetDirection(_leftButtons, Vector3.left);
            _movement += GetDirection(_rightButtons, Vector3.right);

            _movement.Normalize();

            foreach (var element in _shootButtons)
            {
                if (Input.GetKey(element))
                {
                    ShootLaser();
                }
            }
        }

        protected Vector3 GetDirection(List<KeyCode> buttons, Vector3 direction)
        {
            if (!Input.anyKey)
                return Vector3.zero;

            foreach (KeyCode button in buttons)
            {
                if (Input.GetKey(button))
                    return direction;
            }
            return Vector3.zero;
        }

        protected override void Movement()
        {
            MoveThroughScreen();
            if (_movement.magnitude > 0)
            {
                _currentSpeed = _playerSpeed;
                transform.Translate(_movement * _playerSpeed, _movementRelative);

                var playerData = GameContext.CurrentGameData.PlayersData.First(playerData => playerData.Id == Guid);
                playerData.Positions = new[] { transform.position.x, transform.position.y };

                _lastMovement = _movement;
                _movement = Vector3.zero;
                return;
            }

            if (_currentSpeed <= 0)
                return;

            transform.Translate(_lastMovement * _currentSpeed, _movementRelative);
            _currentSpeed *= _inertia;
        }

        private void MoveThroughScreen()
        {
            if (transform.position.x < _minimumWidth)
                transform.position = new Vector3(_maximumWidth, transform.position.y, transform.position.z);

            if (transform.position.x > _maximumWidth)
                transform.position = new Vector3(_minimumWidth, transform.position.y, transform.position.z);

            if (transform.position.y < _minimumHeight)
                transform.position = new Vector3(transform.position.x, _maximumHeight, transform.position.z);

            if (transform.position.y > _maximumHeight)
                transform.position = new Vector3(transform.position.x, _minimumHeight, transform.position.z);
        }
    }
}