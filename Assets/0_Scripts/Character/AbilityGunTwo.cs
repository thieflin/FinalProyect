using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGunTwo : Abilities
{
    public void Start()
    {
        isPurchased = false;
    }

    public override void OnUpdate()
    {
        Attack();
    }
    public override void Attack()
    {
        Debug.Log("Habilidad 1");
        //if (_cs.GetPowerGaugeBarStatus() && isActive)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        _cs.powerGauge -= 50;
        //        Debug.Log("toy funcionando creo");
        //        if (isIdle == true)
        //        {
        //            isIdle = false;
        //            _anim.SetTrigger("GS1");
        //        }
        //    }

        //}
    }
}
