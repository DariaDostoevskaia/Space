using UnityEngine;

public class Game7Object1 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameObject.TryGetComponent(out Game7Object1 component1))
            {
                component1.enabled = false;
                Debug.Log("false");
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    component1.enabled = true;
                    Debug.Log("true");
                }
            }
        }
    }
}