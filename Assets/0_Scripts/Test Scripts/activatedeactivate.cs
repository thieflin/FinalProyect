using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatedeactivate : MonoBehaviour
{
    public GameObject pp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (pp.activeSelf) pp.SetActive(false);
            else pp.SetActive(true);
        }
    }
}
