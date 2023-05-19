using UnityEngine;

public class Game7Object3 : MonoBehaviour
{
    [SerializeField] private Space5OnOff _space5OnOff;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _space5OnOff.enabled = !_space5OnOff.enabled;
        }
        else
        {
            _space5OnOff.enabled = _space5OnOff.enabled;
        }
    }
}