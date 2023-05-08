using UnityEngine;

public class GamePerson : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private float _acceleration;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _position += _targetPosition * _acceleration;
            Debug.Log(_position);
        }
    }
}