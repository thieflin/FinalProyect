using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollision : MonoBehaviour
{
    public int hitDmg;

    //Collision con player
    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<CharStatus>();

        if (player)
            player.TakeDamage(hitDmg);
    }

}
