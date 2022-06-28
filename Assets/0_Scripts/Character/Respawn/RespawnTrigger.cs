using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private bool activated = false;
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            RespawnManager.playerRespawn = this.transform;
            GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            activated = true;
        }
    }
}
