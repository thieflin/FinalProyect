using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScriptTest : MonoBehaviour
{
    public GameObject puenteActivate;
    public GameObject colliderToDestroy;
    public List<GameObject> checkPointRamp = new List<GameObject>();
    public List<GameObject> collidersToSwitch = new List<GameObject>();
    public List<Animator> openPassBlockings;
    public int normalAttackLayer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {
            Debug.Log("ENTREE ACAAAA");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //AudioManager.PlaySound("puzzle");

            if (puenteActivate != null)
                puenteActivate.GetComponent<Animator>().SetTrigger("ActivateBridge");

            //GetComponent<Animator>().SetTrigger("LeverHit");
            GetComponent<Animator>().SetTrigger("ActivateButton");

            if (checkPointRamp.Count > 0)
            {
                foreach (var checkPoint in checkPointRamp)
                {
                    if (!checkPoint.gameObject.activeSelf)
                        checkPoint.SetActive(true);
                    else
                        checkPoint.SetActive(false);
                }

            }

            if(openPassBlockings.Count > 0)
            {
                foreach (var passBlocking in openPassBlockings)
                {
                    passBlocking.SetTrigger("Open");
                }
            }

            if (collidersToSwitch.Count > 0)
            {
                foreach (var collider in collidersToSwitch)
                {
                    collider.GetComponent<BoxCollider>().enabled = !collider.GetComponent<BoxCollider>().enabled;
                }
            }

            Destroy(colliderToDestroy);
        }
    }
}
