using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Sample : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake");
        Debug.LogWarning("Awake");
        Debug.LogError("Awake");


    }
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Awake");
        Debug.LogWarning("Awake");
        Debug.LogError("Awake");

    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }
}
