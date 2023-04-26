using UnityEngine;

public class EnterOn : MonoBehaviour
{
    private EnterOff _keyOn;

    private void Start()
    {
        _keyOn = GetComponent<EnterOff>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _keyOn.enabled = !_keyOn.enabled;
    }
}