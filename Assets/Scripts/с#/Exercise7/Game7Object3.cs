using UnityEngine;

public class Game7Object3 : MonoBehaviour
{
    [SerializeField] public Game7Object3 ssilkaGameObj7;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ssilkaGameObj7.enabled = false;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ssilkaGameObj7.enabled = true;
            }
        }
    }
}