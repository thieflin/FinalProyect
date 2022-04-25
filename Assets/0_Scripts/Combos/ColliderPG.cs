using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPG : MonoBehaviour
{
    public float meleePG;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyStatus>();

        if (enemy)
        {
            EventManager.Instance.Trigger("OnGettingMPG", meleePG);
        }
    }
}
