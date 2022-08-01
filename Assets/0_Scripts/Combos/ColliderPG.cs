using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ColliderPG : MonoBehaviour
{
    //De a cuanto sumo en la power gauge
    public float meleePG;
    private ButtonManager _bm;

    private void Start()
    {
        _bm = FindObjectOfType<ButtonManager>(); //XD
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyStatus>();

        //Si le estoy pegando a enemy, hago el camera shake y stackeo la PowerGauge
        if (enemy)
        {
            CameraShaker.Instance.ShakeOnce(1.2f, 1.2f, .1f, 1f);

            if(_bm.meleeUpgrade >= 1)
                EventManager.Instance.Trigger("OnGettingMPG", meleePG);
            if (_bm.hybridUpgrade >= 1)
                EventManager.Instance.Trigger("OnGettingHPG", meleePG/2);
        }
    }
}
