using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraScriptTest : MonoBehaviour
{
    public GameObject puenteActivate;
    public GameObject colliderToDestroy;
    public List<GameObject> checkPointRamp = new List<GameObject>();
    public List<GameObject> collidersToSwitch = new List<GameObject>();
    public int normalAttackLayer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //AudioManager.PlaySound("puzzle");
            puenteActivate.GetComponent<Animator>().SetTrigger("Work");
            GetComponent<Animator>().SetTrigger("LeverHit");

            if(checkPointRamp.Count > 0)
            {
                foreach (var checkPoint in checkPointRamp)
                {
                    if (!checkPoint.gameObject.activeSelf)
                        checkPoint.SetActive(true);
                    else
                        checkPoint.SetActive(false);
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
