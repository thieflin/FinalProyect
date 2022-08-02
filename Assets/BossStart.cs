using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public Boss boss;
    public GameObject bossUI;
    public GameObject collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.randomAttack = 0;
            bossUI.SetActive(true);

            collider.GetComponent<BoxCollider>().enabled = true;
        }
    }


}
