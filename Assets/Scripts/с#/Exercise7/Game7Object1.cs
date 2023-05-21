using UnityEngine;

public class Game7Object1 : MonoBehaviour
{
    [SerializeField] private Space5OnOff _space5OnOff;
    //private GameObject _gameObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //_gameObject.TryGetComponent(out _space5OnOff);
            _space5OnOff.enabled = !_space5OnOff.enabled;
        }
    }
}