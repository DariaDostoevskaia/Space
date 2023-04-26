using UnityEngine;

public class EnterOff : MonoBehaviour
{
    private EnterOn _keyOff;

    private void Start()
    {
        _keyOff = GetComponent<EnterOn>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            _keyOff.enabled = !_keyOff.enabled;
    }
}