using UnityEngine;

public class GamePerson2Position : MonoBehaviour
{
    [SerializeField] private Vector3 _position;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _position.y += _position.x;
            Debug.Log($"{_position.x}, {_position.y}, {_position.z}");
        }
    }
}