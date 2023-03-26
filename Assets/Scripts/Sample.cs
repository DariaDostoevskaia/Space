using System;
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
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
    }
}