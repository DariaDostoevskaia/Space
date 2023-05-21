using UnityEngine;

public class Game7Object2 : MonoBehaviour
{
    [SerializeField] private Space5OnOff _space5OnOff;

    private void Update()
    {
        var obj = FindObjectOfType<Space5OnOff>();

        if (Input.GetKeyDown(KeyCode.Return))
            obj.enabled = !obj.enabled;
    }
}