using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    [SerializeField] private List<KeyCode> _Enter;

    private void Start()
    {
        //var sample = GetComponent<Sample>();

        //sample.
        TestCube6();
        //sample.enabled = false; //отключить сэмпл
        //Destroy(gameObject);
    }

    public virtual void TestCube6()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
    }
}