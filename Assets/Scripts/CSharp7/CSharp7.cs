using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSharp7 : MonoBehaviour
{
    [SerializeField] private int _skipCount;
    [SerializeField] private int _takeCount;

    private int _vectorCount = 5;
    private List<int> xPositions;

    public void Start()
    {
        var list = new List<Vector3>();
        GetXPositions(list);
    }

    public List<int> GetXPositions(List<Vector3> vectors)
    {
        xPositions = vectors
          .Where(vector => vector.x > _vectorCount)
          .Select(vector => (int)vector.x)
          .ToList();
        GetArray();
        return xPositions;
    }

    private void GetArray()
    {
        // Exercise 3.Skip, ToArray
        var array = xPositions.ToArray();

        if (_skipCount >= 0
            && _skipCount < array.Length)
        {
            var skipNumber = array.Skip(_skipCount);
            foreach (int x in skipNumber)
                Debug.Log($"{x} ");
        }
        // Exercise 4. Take
        if (_takeCount >= 0
           && _takeCount < array.Length)
        {
            var takeNumber = array.Take(_takeCount);
            foreach (int y in takeNumber)
                Debug.Log($"{y} ");
        }
    }
}