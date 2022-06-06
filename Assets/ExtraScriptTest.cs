using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScriptTest : MonoBehaviour
{
    public GameObject puenteActivate;
    public GameObject colliderToDestroy;
    public int normalAttackLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //AudioManager.PlaySound("puzzle");
            puenteActivate.GetComponent<Animator>().SetTrigger("Work");
            GetComponent<Animator>().SetTrigger("LeverHit");

            Destroy(colliderToDestroy);
        }
    }
}
