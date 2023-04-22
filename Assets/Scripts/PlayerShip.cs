using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerShip : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _inertia = 0.9f;
    [SerializeField] private List<KeyCode> _upButtons;
    [SerializeField] private List<KeyCode> _downButtons;
    [SerializeField] private List<KeyCode> _leftButtons;
    [SerializeField] private List<KeyCode> _rightButtons;

    private float _currentSpeed;
    private Vector3 _lastMovement;
    private Vector3 _movement;

    private void Update()
    {
        _movement += GetDirection(_upButtons, Vector3.up);
        _movement += GetDirection(_downButtons, Vector3.down);
        _movement += GetDirection(_leftButtons, Vector3.left);
        _movement += GetDirection(_rightButtons, Vector3.right);

        _movement.Normalize();

        HandleTargetRotation();
    }

    private void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    protected abstract void Rotation();

    protected abstract void HandleTargetRotation();

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

    private void Movement()
    {
        if (_movement.magnitude > 0)
        {
            _currentSpeed = _playerSpeed;
            transform.Translate(_movement * _playerSpeed, Space.World);
            _lastMovement = _movement;
            _movement = Vector3.zero;
            return;
        }

        if (_currentSpeed <= 0)
            return;

        transform.Translate(_lastMovement * _currentSpeed, Space.World);
        _currentSpeed *= _inertia;
    }
}