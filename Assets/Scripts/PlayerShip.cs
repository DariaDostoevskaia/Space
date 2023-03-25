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
    public SpriteRenderer sprite;
    public Animator charAnimator;

    private void Start()
    {
        //_items
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);

        if (Input.GetButton("Horizontal"))
        {
            Movement();
            charAnimator.SetInteger("State", 1);
        }
        else
        {
            charAnimator.SetInteger("State", 0);
        }
        
    }
    private void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    private void Movement()
    {
        Vector3 tempVector = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempVector, _currentSpeed*Time.deltaTime);
        if (tempVector.x < 0)
        {
            sprite.flipX = true;
        }
        else 
        {
            sprite.flipX = false;
        }
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
