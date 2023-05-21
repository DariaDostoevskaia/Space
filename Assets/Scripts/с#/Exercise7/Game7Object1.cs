using UnityEngine;

public class Game7Object1 : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private Space5OnOff _space5OnOff;

    private void Start()
    {
        if (!_gameObject.TryGetComponent<Space5OnOff>(out _space5OnOff))
            Debug.LogError("Component missing.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _space5OnOff.enabled = !_space5OnOff.enabled;
        }
    }
}