using UnityEngine;

public class Switch : MonoBehaviour
{
    private Switch _keyOff;

    private void Start()
    {
        _keyOff = GetComponent<Switch>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _keyOff.enabled = !_keyOff.enabled;
    }
}