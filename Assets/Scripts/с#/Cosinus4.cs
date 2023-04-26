using UnityEngine;

public class Cosinus4 : MonoBehaviour
{
    [SerializeField] private float _cosinus;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _cosinus = Mathf.Cos(_cosinus);
            Debug.Log(_cosinus);
        }
    }
}