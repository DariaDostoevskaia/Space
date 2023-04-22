using UnityEngine;

public class Enter : MonoBehaviour
{
    private void Start()
    {
        var sample = GetComponent<Sample>();
        sample.TestCube6();
        //sample.enabled = false; //отключить сэмпл
        //Destroy(gameObject);
    }
}