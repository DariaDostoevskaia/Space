using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    private void Awake()
    {
    }

    private void Start()
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

        var positions = new List<Vector3>(20);
        var number = Random.Range(0, 10);
        for (int i = 0; i < 20; i++)
        {
        }
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
    }
}