using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    public Boss boss;
    public GameObject bossUI;
    public GameObject collider;
    public GameObject firstPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.gameObject.SetActive(true);
            boss.transform.position = firstPos.transform.position;
            boss.shooting = false;
            boss.attacking = false;
            boss.grabing = false;
            boss.startShooting = false;
            boss.randomAttack = 0;
            bossUI.SetActive(true);

            collider.GetComponent<BoxCollider>().enabled = true;
        }
    }


}
