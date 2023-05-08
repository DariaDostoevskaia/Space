using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sample : MonoBehaviour
{
    private void Start()
    {
        //DoExercise1();
        //DoExercise2();
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
        for (int i = 1; i < positions.Count; i++)
        {
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