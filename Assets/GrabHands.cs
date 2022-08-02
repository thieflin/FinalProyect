using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHands : MonoBehaviour
{
    BoxCollider myBoxCollider;

    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider>();
    }

    public void ChangeState()
    {
        myBoxCollider.enabled = !myBoxCollider.enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharStatus>().TakeDamage(20);
        }
    }
}
