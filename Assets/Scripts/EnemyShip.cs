using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _inertia = 0.9f;

    [SerializeField] private List<KeyCode> _upArrows;
    [SerializeField] private List<KeyCode> _downArrows;
    [SerializeField] private List<KeyCode> _leftArrows;
    [SerializeField] private List<KeyCode> _rightArrows;

    private float _currentSpeed;
    private Vector3 _lastMovement;
    private Vector3 _enemyMousePosition;

    private Vector3 _movement;

    private void Start()
    {
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.L))
        //    transform.rotation *= Quaternion.Euler(0f, 360f * Time.deltaTime, 0f);

        _movement += GetDirection(_upArrows, Vector3.up);
        _movement += GetDirection(_downArrows, Vector3.down);
        _movement += GetDirection(_leftArrows, Vector3.left);
        _movement += GetDirection(_rightArrows, Vector3.right);

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
        var dx = shipPosition.x - _enemyMousePosition.x;
        var dy = shipPosition.y - _enemyMousePosition.y;
        var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        transform.rotation = rotation;
    }

    //private void Flip()
    //{
    //    var dir = transform.position - _moveSpots[_randomSpot].position;
    //    if (dir.x < 0.0f)
    //    {
    //        _spriteRenderer.flipX = true;
    //        _isForward = true;
    //    }
    //    else
    //    {
    //        _spriteRenderer.flipX = false;
    //        _isForward = false;
    //    }
    //}
}