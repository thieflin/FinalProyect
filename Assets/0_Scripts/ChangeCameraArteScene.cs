using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeCameraArteScene : MonoBehaviour
{
    public Camera[] CameraTransform;
    public bool currentCamera;
    void Start()
    {
        CameraTransform[0].gameObject.SetActive(true);
        CameraTransform[1].gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !currentCamera)
        {
            CameraTransform[0].gameObject.SetActive(false);
            CameraTransform[1].gameObject.SetActive(true);
            currentCamera = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && currentCamera)
        {
            CameraTransform[0].gameObject.SetActive(true);
            CameraTransform[1].gameObject.SetActive(false);
            currentCamera = false;
        }
    }
}
