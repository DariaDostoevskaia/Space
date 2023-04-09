using UnityEngine;

public class Enter : MonoBehaviour
{
    private void Start()
    {
        var sample = GetComponent<Sample>();
        sample.Test();
        sample.enabled = false; //отключить сэмпл
        Destroy(gameObject);
    }
}