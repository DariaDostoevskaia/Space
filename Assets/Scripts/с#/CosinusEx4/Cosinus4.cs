using UnityEngine;

public class Cosinus4 : MonoBehaviour
{
    [SerializeField] private float _cosinus;
    private Vector3 _point;

    private void Update()
    {
        Vector3 point = transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            _cosinus = Mathf.Cos(_point.x);
            Debug.Log(_cosinus);
        }
    }
}