using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectable : MonoBehaviour, ICollectible
{
    [SerializeField] private int expValue;


    public void OnPickUp()
    {
        //Deberia correr una mini animacion
        EventManager.Instance.Trigger("OnGettingExp", expValue);
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
