using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPColletibles : MonoBehaviour, ICollectible
{
    [SerializeField] private int spValue;


    public void OnPickUp()
    {
        //Deberia correr una mini animacion
        EventManager.Instance.Trigger("OnEarningSP", spValue);
        AudioManager.PlaySound("collect");
        Destroy(gameObject);
    }



    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player)
        {

            OnPickUp();
        }
    }
}
