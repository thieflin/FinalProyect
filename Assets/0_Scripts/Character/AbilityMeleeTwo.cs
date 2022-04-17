using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeleeTwo : Abilities
{
    public override void OnUpdate()
    {
        Attack();

    }
    public override void Attack()
    {
        Debug.Log("Melee2");
        //if (_cs.GetPowerGaugeBarStatus() && isActive && isOnUse)
        //{

        //    _cs.powerGauge -= 50;
        //    Debug.Log("toy funcionando creo");
        //    if (isIdle == true)
        //    {
        //        isIdle = false;
        //        _anim.SetTrigger("MS1");
        //    }



        //}
    }
}
