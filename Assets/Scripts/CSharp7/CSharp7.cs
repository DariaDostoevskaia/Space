using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSharp7 : MonoBehaviour
{
    private int _skipCount = 1;
    private int _takeCount = 2;

    private int _vectorCount = 5;
    private List<int> xPositions;

    public void Start()
    {
        var list = new List<Vector3>(_vectorCount);
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

        int[] arraySkip = new int[] { };
        List<int> arrayConvertSkip = new List<int>(arraySkip);

        arrayConvertSkip = (List<int>)xPositions.ToArray()
            .Skip(_skipCount);

        if (_skipCount >= 0
            && _skipCount < xPositions.Count)
        {
            foreach (int x in arrayConvertSkip)
                Debug.Log($"{x} ");
        }

        // Exercise 4. Take

        int[] arrayTake = new int[] { };
        List<int> arrayConvertTake = new List<int>(arrayTake);

        if (_takeCount >= 0
            && _takeCount < xPositions.Count)
        {
            arrayConvertTake = (List<int>)xPositions.Take(_takeCount);

            foreach (int y in arrayConvertTake)
                Debug.Log($"{y} ");
        }
    }
}