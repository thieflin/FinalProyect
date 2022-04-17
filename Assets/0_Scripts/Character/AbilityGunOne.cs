using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGunOne : Abilities
{
    public GameObject shieldCollider;
    public bool shieldActive;
    public void GetShield(params object[] parametersk)
    {
        shieldActive = true;
    }

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

    public  void FinishAbility()
    {

        _anim.SetTrigger("Idle");
        isIdle = true;

    }

    public  void ActivateColliderAndAnim()
    {
        actionCollider.SetActive(true);
    }

    public  void DeactivateCollider()
    {
        actionCollider.SetActive(false);
    }

    public void ActivateShieldOnAttack()
    {
        if (shieldActive)
        {
            shieldCollider.SetActive(true);
            StartCoroutine(ShieldDecay());
        }
    }

    IEnumerator ShieldDecay()
    {
        yield return new WaitForSeconds(3);
        shieldCollider.SetActive(false);
    }
}
