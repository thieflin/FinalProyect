using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Transform[] myCamera = null;
    private bool active = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && active == false)
        {
            active = true;
            myCamera[0].gameObject.SetActive(false);
            myCamera[1].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.P) && active == true)
        {
            active = false;
            myCamera[1].gameObject.SetActive(false);
            myCamera[0].gameObject.SetActive(true);
        }
    }
}
