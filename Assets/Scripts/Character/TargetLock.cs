using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    public List<Enemy> enemiesClose = new List<Enemy>();

    public PlayerMovement player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (enemiesClose.Count > 0 && !player.isTargeting)
                LookForClosestEnemy();
            else if (enemiesClose.Count > 0 && player.isTargeting)
                player.isTargeting = false;
            else
                Debug.Log("No hay enemigos cercanos");
        }
    }

    void LookForClosestEnemy()
    {
        Enemy closestEnemy = null;

        float closestDistance = (enemiesClose[0].transform.position - player.transform.position).magnitude;

        foreach (var enemy in enemiesClose)
        {
            float distance = (enemy.transform.position - player.transform.position).magnitude;

            if(distance <= closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        player.TargetEnemy(closestEnemy);

    }

    //AGREGAR Y QUITAR ENEMIGOS DE LA LISTA DE ENEMIGOS CERCANOS
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemiesClose.Contains(other.GetComponent<Enemy>()))
        {
            enemiesClose.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemiesClose.Contains(other.GetComponent<Enemy>()))
        {
            enemiesClose.Remove(other.GetComponent<Enemy>());
        }
    }
}
