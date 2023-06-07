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
        GetArray();
    }

    public List<int> GetXPositions(List<Vector3> vectors)
    {
        xPositions = vectors
          .Where(vector => vector.x > _vectorCount)
          .Select(vector => (int)vector.x)
          .ToList();
        return xPositions;
    }

    private void GetArray()
    {
        // Exercise 3.Skip, ToArray

        var array = xPositions.ToArray()
            .Skip(_skipCount);

        if (_skipCount >= 0
            && _skipCount < xPositions.Count)
        {
            foreach (int x in array)
                Debug.Log($"{x} ");
        }

        // Exercise 4. Take

        if (_takeCount >= 0
            && _takeCount < xPositions.Count)
        {
            var arrayTake = xPositions.Take(_takeCount);

            foreach (int y in arrayTake)
                Debug.Log($"{y} ");
        }
    }
}