using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _inertia = 0.9f;
    [SerializeField] private List<KeyCode> _upButtons;
    [SerializeField] private List<KeyCode> _downButtons;
    [SerializeField] private List<KeyCode> _leftButtons;
    [SerializeField] private List<KeyCode> _rightButtons;

    private float _currentSpeed;
    private Vector3 _lastMovement;
    private Vector3 _mousePosition;

    private Vector3 _movement;

    private void Start()
    {
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        _movement += GetDirection(_upButtons, Vector3.up);
        _movement += GetDirection(_downButtons, Vector3.down);
        _movement += GetDirection(_leftButtons, Vector3.left);
        _movement += GetDirection(_rightButtons, Vector3.right);

        _movement.Normalize();
    }

    private void FixedUpdate()
    {
        Rotation();
        Movement();
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

    private Vector3 GetDirection(List<KeyCode> buttons, Vector3 direction)
    {
        foreach (KeyCode button in buttons)
        {
            if (Input.GetKey(button))
            {
                return direction;
            }
        }
        return Vector3.zero;
    }

    private void Rotation()
    {
        var shipPosition = transform.position;
        var dx = shipPosition.x - _mousePosition.x;
        var dy = shipPosition.y - _mousePosition.y;
        var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        transform.rotation = rotation;
    }
}