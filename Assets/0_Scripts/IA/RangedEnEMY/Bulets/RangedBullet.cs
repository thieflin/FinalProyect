using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBullet : MonoBehaviour
{
    public float speed;
    public int bulletDmg;
    public float destroyTimer;

    //Movimiento y destruccion de la bala
    void Update()
    {
        transform.position += transform.forward* speed * Time.deltaTime;

        destroyTimer -= Time.deltaTime;
        if (destroyTimer < 0)
            Destroy(gameObject);
    }

    //Collision con player
    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<CharStatus>();

        if (player)
            player.TakeDamage(bulletDmg);
    }

}
