using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSharp7 : MonoBehaviour
{
    [SerializeField] private int _skipCount;
    [SerializeField] private int _takeCount;

    public List<int> GetXPositions(List<Vector3> vectors)
    {
        var xPositions = vectors
           .Where(vector => vector.x > 5)
           .Select(vector => (int)vector.x)
           .ToList();
        return xPositions;

        void GetArray()
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
}