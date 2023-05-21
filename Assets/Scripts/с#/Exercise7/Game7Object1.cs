using UnityEngine;

public class Game7Object1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameObject.TryGetComponent(out Space5OnOff _component1))

                _component1.enabled = !_component1.enabled;
        }
    }
}