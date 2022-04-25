using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRPG : MonoBehaviour
{
    public float meleePG;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyStatus>();

        if (enemy)
        {
            EventManager.Instance.Trigger("OnGettingRPG", meleePG);
        }
    }

    
}
