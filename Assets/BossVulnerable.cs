using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVulnerable : MonoBehaviour
{
    public Boss boss;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            AudioManager.PlaySound("RobotHitted");
            boss.TakeDmg(3);
        }
        if (other.gameObject.layer == 23)
        {
            AudioManager.PlaySound("RobotHitted");
            boss.TakeDmg(5);
        }
    }
}
