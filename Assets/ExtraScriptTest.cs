using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScriptTest : MonoBehaviour
{
    public GameObject colliderTest;
    public int normalAttackLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            Destroy(colliderTest);
        }
    }
}
