using System;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    private void Start()
    {
        //DoExercise1();
        DoExercise2();
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

        var x = 0;
        var y = 0;
        var z = 0;

        for (int i = 0; i < positions.Capacity; i++)
        {
            x = UnityEngine.Random.Range(0, 11);
            y = UnityEngine.Random.Range(0, 11);
            z = UnityEngine.Random.Range(0, 11);

            var position = new Vector3(x, y, z);
            positions.Add(position);
        }
        var positionsSecond = new List<Vector3>(19);
        for (int i = 0; i < positions.Capacity; i++)
        {
            if (i == 0)
            {
                positionsSecond.Add(positions[0]);
                continue;
            }
            var temp = positions[i] + positions[i - 1];
            positionsSecond.Add(temp);
        }
        for (int i = 0; i < positionsSecond.Capacity; i++)
        {
            if (x + y + z > 5)
            {
                positionsSecond.RemoveAt(i);
                positionsSecond.Capacity--;
            }
        }
        for (int i = 0; i < positionsSecond.Capacity; i++)
        {
            Debug.Log(positionsSecond[i]);
        }
    }
}