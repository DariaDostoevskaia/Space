using UnityEngine;

public class Game7Object2 : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var obj = FindObjectOfType<Game7Object2>();
            obj.enabled = false;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                obj.enabled = true;
            }
        }
    }
}