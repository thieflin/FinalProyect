using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGunOne : Abilities
{
    private void Start()
    {
        _anim = GetComponent<Animator>();
        isIdle = true;
        actionCollider.SetActive(false);
        isActive = false;
    }
    private void Update()
    {
        Attack();
        Debug.Log(isActive);
    }
    public override void Attack()
    {
        if (_cs.GetPowerGaugeBarStatus() && isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _cs.powerGauge -= 50;
                Debug.Log("toy funcionando creo");
                if (isIdle == true)
                {
                    isIdle = false;
                    _anim.SetTrigger("GS1");
                }
            }

        }
    }

    public override void FinishAbility()
    {

        _anim.SetTrigger("Idle");
        isIdle = true;

    }

    public override void ActivateColliderAndAnim()
    {
        actionCollider.SetActive(true);
    }

    public override void DeactivateCollider()
    {
        actionCollider.SetActive(false);
    }
}
