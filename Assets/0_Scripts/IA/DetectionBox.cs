using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBox : MonoBehaviour
{
    public bool playerInBox;

    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player) playerInBox = true;

        Debug.Log("Entro el player en la zona");

    }

    public void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();

        if (player) playerInBox = false;
        Debug.Log("Salio el player de la zona");
    }



}
