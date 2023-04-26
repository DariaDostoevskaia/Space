using UnityEngine;

public class Space5OnOff : MonoBehaviour
{
    [SerializeField] private GameObject _cube;

    private void Update()
    {
        TestCube6();
    }

    public virtual void TestCube6()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_cube.activeSelf)
                _cube.SetActive(true);
            else
                _cube.SetActive(false);
        }
    }
}