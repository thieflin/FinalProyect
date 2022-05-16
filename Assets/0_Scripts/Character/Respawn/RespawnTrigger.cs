using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private bool activated = false;

    public List<Enemy> enemiesToRespawn = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            RespawnManager.playerRespawnNumber++;
            GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            activated = true;
        }
    }
}
