using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoButtons : MonoBehaviour
{
    public GameObject[] puentesToActivate;
    public static int buttonsPressed;

    public GameObject[] colliderToDestroy;

    public GameObject colliderBlock;

    public List<Animator> openPassBlockings;

    public int normalAttackLayer;

    private void Start()
    {
        buttonsPressed = 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {

            buttonsPressed++;

            gameObject.GetComponent<BoxCollider>().enabled = false;
            //AudioManager.PlaySound("puzzle");


            if (puentesToActivate.Length >= 0)
            {
                foreach (var puentes in puentesToActivate)
                {
                    if (buttonsPressed == 1)
                    {
                        puentes.GetComponent<Animator>().SetTrigger("Part1");
                    }
                    else if(buttonsPressed == 2)
                    {
                        puentes.GetComponent<Animator>().SetTrigger("Part2");

                        foreach (var collider in colliderToDestroy)
                        {
                            Destroy(collider.gameObject);
                        }
                    }
                }
            }

            //GetComponent<Animator>().SetTrigger("LeverHit");
            GetComponent<Animator>().SetTrigger("ActivateButton");


            if (openPassBlockings.Count > 0)
            {
                foreach (var passBlocking in openPassBlockings)
                {
                    passBlocking.SetTrigger("Open");
                }
            }

            Destroy(colliderBlock);
        }
    }
}
