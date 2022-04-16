using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeleeOne : Abilities
{
    public int admg;

    public AbilityMeleeOne(int dmg)
    {
        admg = dmg;
    }

    public override void OnUpdate()
    {
        Attack();

    }
    public override void Attack()
    {
        Debug.Log("estoy atacando locura comsica con el melee");
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

    public void FinishAbility()
    {
        _anim.SetTrigger("Idle");
        isIdle = true;
    }

    public void ActivateColliderAndAnim1() //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        actionCollider.SetActive(true);
    }
    public void DeactivateCollider1()
    {
        actionCollider.SetActive(false);
    }
}
