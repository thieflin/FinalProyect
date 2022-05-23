using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ColliderPG : MonoBehaviour
{
    public float meleePG;


    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyStatus>();

        if (enemy)
        {
            CameraShaker.Instance.ShakeOnce(1.2f, 1.2f, .1f, 1f);
            EventManager.Instance.Trigger("OnGettingMPG", meleePG);
        }
    }
}
