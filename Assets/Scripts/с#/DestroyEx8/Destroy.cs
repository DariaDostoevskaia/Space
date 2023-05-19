using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
            Destroy(_gameObject);
    }
}