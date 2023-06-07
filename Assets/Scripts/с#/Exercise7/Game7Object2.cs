using UnityEngine;

public class Game7Object2 : MonoBehaviour
{
    [SerializeField] private Space5OnOff _space5OnOff;
    private Space5OnOff obj;

    private void Start()
    {
        obj = FindObjectOfType<Space5OnOff>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            obj.enabled = !obj.enabled;
    }
}