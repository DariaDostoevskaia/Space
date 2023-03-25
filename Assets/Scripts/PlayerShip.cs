using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{

    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private List<KeyCode> _upButtons;
    [SerializeField] private List<KeyCode> _downButtons;
    [SerializeField] private List<KeyCode> _leftButtons;
    [SerializeField] private List<KeyCode> _rightButtons;

    private float _currentSpeed;
    private Vector3 _lastMovement;
    private Vector3 _mousePosition;

    private void Start()
    {
        
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
    }
    private void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    private void Movement()
    {
       
    }

    private void Rotation()
    {
        var shipPosition = transform.position;
        var dx = shipPosition.x - _mousePosition.x;
        var dy = shipPosition.y - _mousePosition.y;
        var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(new Vector3(0,0,angle+90));
        transform.rotation = rotation;
    }
}
