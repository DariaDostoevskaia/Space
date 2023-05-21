using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSharp7 : MonoBehaviour
{
    public List<int> GetXPositions(List<Vector3> vectors)
    {
        var xPositions = vectors
           .Where(vector => vector.x > 5)
           .Select(vector => (int)vector.x)
           .ToList();
        return xPositions;
    }
}