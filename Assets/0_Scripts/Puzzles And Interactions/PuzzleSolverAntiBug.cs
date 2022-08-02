using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolverAntiBug : MonoBehaviour
{

    public List<GameObject> allBlockingColliders = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            foreach (var item in allBlockingColliders)
            {
                item.SetActive(false);
            }
        }
    }
}
