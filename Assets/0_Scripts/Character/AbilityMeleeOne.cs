using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeleeOne : Abilities
{
    private void Start()
    {
        _anim = GetComponent<Animator>();
        isIdle = true;
        isActive = false;
        _cs = GetComponent<CharStatus>();
        actionCollider.SetActive(false);
    }
    private void Update()
    {
        Attack();

    }
    public override void Attack()
    {
        if (_cs.GetPowerGaugeBarStatus() && isActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _cs.powerGauge -= 50;
                Debug.Log("toy funcionando creo");
                if (isIdle == true)
                {
                    isIdle = false;
                    _anim.SetTrigger("MS1");
                }

            }

        }
    }

    public override void FinishAbility()
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
