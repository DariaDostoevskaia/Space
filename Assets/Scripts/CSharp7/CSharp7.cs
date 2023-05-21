using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CSharp7 : MonoBehaviour
{
    private void Start()
    {
        //List<int> numQuery2 = (from num in numbers where (num % 2) == 0 select num)
        //    .ToList();
        var xPositions = new List<int>();
        var list = Enumerable.Range(1, 10)
            .Select(i => i * i)
            .ToList();
    }

    //public List<int> GetXPositions(List<Vector3> vectors)
    //{
    //var xPositions = new List<int>();
    //for (int i = 0; i < vectors.Length; i++)
    //{
    //    if (vectors[i].x > 5)
    //        xPositions.Add(letters[i].x);
    //}
    //return xPositions;
    //}

    //public IEnumerable<int> GetNewLetterIds_LinqWay(IEnumerable<object> letters)
    //{
    //    return letters
    //        .Where(letter => letter
    //        .IsNew)
    //        .Select(letter => letter.Id);
    //}
}