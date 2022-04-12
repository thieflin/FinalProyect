using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivateBridge : MonoBehaviour
{
    public Animator animBridge;
    public GameObject desactivateWallCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animBridge.SetTrigger("ActivateBridge");
            desactivateWallCollider.SetActive(false);
        }
    }
}
