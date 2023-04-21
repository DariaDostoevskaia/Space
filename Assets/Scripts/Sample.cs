using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sample : MonoBehaviour
{
    [SerializeField] private List<KeyCode> jump;
    [SerializeField] private float x = 1f;
    [SerializeField] private float y = 8f;

    public float _speed = 0.1f;

    private float _time;
    private float _lerp;
    private double _period;
    private Vector3 start;
    private Vector3 target;
    private float distance;
    private bool goRight;

    //Пояснение из интернета:

    //Взял за основу функцию f(x) = (1 - cos(a * x) ) / 2  , где a - параметр
    //В нашем случае скорость. Чем больше a, тем более "сплюснутая" функция, тем
    //быстрее пила

    //Область значения f = [0 , 1]
    //Аргумент функции x - время
    //Значение функции - пройденный путь пилы
    //Период f = 2PI / a

    private void Start()
    {
        //DoExercise1();
        //DoExercise2();
        start = transform.position; // point A
        target = start + transform.right * distance * (goRight ? 1 : -1); // point B
        _period = 2 * Mathf.PI / _speed;
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

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TestCube();
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _period)
            _time = 0;
        _lerp = (float)(1 - Mathf.Cos(_speed * _time)) / 2;

        transform.position = Vector3.Lerp(start, target, _lerp);
    }

    private void TestCube()
    {
        var probel = new Vector3(x, y, 0);
        gameObject.transform.position = probel;
    }
}