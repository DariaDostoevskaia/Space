using System.Collections.Generic;
using UnityEngine;

public class Enter : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private void Start()
    {
        //var sample = GetComponent<Sample>();

        //sample.TestCube6();
        //sample.enabled = false; //отключить сэмпл
        //Destroy(gameObject);
    }

    private void Update()
    {
        TestCube6();
    }

    public virtual void TestCube6()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_cube.activeSelf)
                _cube.SetActive(true);
            else
                _cube.SetActive(false);
        }
    }
}