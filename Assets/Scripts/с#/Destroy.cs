using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
            Destroy(gameObject);
    }
}