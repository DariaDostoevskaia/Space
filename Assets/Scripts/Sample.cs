using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sample : MonoBehaviour
{
    [SerializeField] private List<KeyCode> _jump;
    [SerializeField] private List<KeyCode> _accelerations;
    [SerializeField] private float x = 1f;
    [SerializeField] private float y = 8f;

    [SerializeField] private float _acceleration = 2f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _resultSpeed;

    [SerializeField] private float _cosinus;
    private float angle = 360;

    private void Start()
    {
        //DoExercise1();
        //DoExercise2();
        //TestCube4Cosinus();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TestCube2();
        }

        if (Input.GetKey(KeyCode.W))
        {
            TestCube3();
        }

        //if (Input.GetMouseButton(0))
        //{
        TestCube4Cosinus();
        //}

        TestCube6();
    }

    private void TestCube2()
    {
        var probel = new Vector3(x, y, 0);
        gameObject.transform.position = probel;
    }

    private void TestCube3()
    {
        _resultSpeed = _speed * _acceleration;
    }

    private void TestCube4Cosinus()
    {
        Vector3 point = transform.position;
        angle *= Mathf.Deg2Rad;

        for (int i = 1; i <= 1; i++)
        {
            float _z = transform.position.z + Mathf.Cos(angle / i);
            float _x = transform.position.x + Mathf.Sin(angle / i);
            point.x = _x;
            point.z = _z;
        }
        //Instantiate(gameObject, point, Quaternion.identity);
        _cosinus = Mathf.Cos(angle);
    }

    public virtual void TestCube6()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
        }
    }

    private void DoExercise1()
    {
        var items = new List<string>();
        for (int i = 1; i <= 10; i++)
        {
            items.Add($"item_{i}");
        }
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i]);
        }
    }

    private void DoExercise2()
    {
        var positions = new List<Vector3>(20);

        for (int i = 0; i < positions.Capacity; i++)
        {
            var x = Random.Range(0, 11);
            var y = Random.Range(0, 11);
            var z = Random.Range(0, 11);

            var position = new Vector3(x, y, z);
            positions.Add(position);
        }
        for (int i = 0; i < positions.Count; i++)
        {
            if (i == 0)
            {
                continue;
            }
            positions[i - 1] += positions[i];
        }
        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].x > 5)
            {
                positions.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < positions.Count; i++)
        {
            Debug.Log(positions[i]);
        }
    }
}