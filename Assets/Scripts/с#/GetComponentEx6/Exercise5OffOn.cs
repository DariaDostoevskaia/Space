using UnityEngine;

public class Exercise5OffOn : MonoBehaviour
{
    private Switch _keyOn;

    private void Start()
    {
        _keyOn = GetComponent<Switch>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _keyOn.enabled = !_keyOn.enabled;
    }
}