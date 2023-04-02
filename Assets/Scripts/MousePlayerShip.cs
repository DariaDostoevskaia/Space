using UnityEngine;

public class MousePlayerShip : PlayerShip
{
    private Vector3 _mousePosition;
    private Quaternion _rotation;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    protected override void HandleTargetRotation()
    {
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        var shipPosition = transform.position;
        var dx = shipPosition.x - _mousePosition.x;
        var dy = shipPosition.y - _mousePosition.y;
        var angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        var euler = new Vector3(0, 0, angle + 90);
        _rotation = Quaternion.Euler(euler);
    }

    protected override void Rotation()
    {
        transform.rotation = _rotation;
    }
}