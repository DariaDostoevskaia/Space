using UnityEngine;

public class Space5OnOff : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private void Update()
    {
        IsSwitchComponent();
    }

    private void IsSwitchComponent()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            _cube.SetActive(!_cube.activeSelf);
    }
}