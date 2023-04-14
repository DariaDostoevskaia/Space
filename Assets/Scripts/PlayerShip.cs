using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public abstract class PlayerShip : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _inertia = 0.9f;
    [SerializeField] private bool _movementBowShip;

    [SerializeField] private List<KeyCode> _upButtons;
    [SerializeField] private List<KeyCode> _downButtons;
    [SerializeField] private List<KeyCode> _leftButtons;
    [SerializeField] private List<KeyCode> _rightButtons;

    [SerializeField] private Transform _laser;
    [SerializeField] private float _laserDistance;
    [SerializeField] private float _timeBetweenFires;
    [SerializeField] private float _laserSpeed = 2.0f;
    [SerializeField] private List<KeyCode> _shootButtons;

    private float _timeNextFire;
    private float _currentSpeed;
    public float _lifeTimeLaser = 2.0f;
    private Vector3 _lastMovement;
    private Vector3 _movement;
    private Space _movementRelative;

    private void Start()
    {
        _movementRelative = _movementBowShip
            ? Space.Self
            : Space.World;

        Destroy(_laser, _lifeTimeLaser);
    }

    private void Update()
    {
        _movement += GetDirection(_upButtons, Vector3.up);
        _movement += GetDirection(_downButtons, Vector3.down);
        _movement += GetDirection(_leftButtons, Vector3.left);
        _movement += GetDirection(_rightButtons, Vector3.right);

        _movement.Normalize();

        HandleTargetRotation();

        foreach (var element in _shootButtons)
        {
            if (Input.GetKey(element)
                && _timeNextFire < 0)
            {
                _timeNextFire = _timeBetweenFires;
                ShootLaser();
            }
        }
        _timeNextFire -= Time.deltaTime;
    }

    private void ShootLaser()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.Translate(Vector3.up * Time.deltaTime * _laserSpeed);
            float positionX = transform.position.x;
            float positionY = transform.position.y;
            Instantiate(_laser, new Vector3(positionX, positionY, 0), transform.rotation);
        }
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
            transform.Translate(_movement * _playerSpeed, _movementRelative);
            _lastMovement = _movement;
            _movement = Vector3.zero;
            return;
        }

        if (_currentSpeed <= 0)
            return;

        transform.Translate(_lastMovement * _currentSpeed, _movementRelative);
        _currentSpeed *= _inertia;
    }
}