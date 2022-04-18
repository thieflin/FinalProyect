using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGunTwo : Abilities
{
    public override void OnUpdate()
    {
        Attack();

    }
    public override void Attack()
    {
        Debug.Log("GunOne");

        if (isIdle == true)
        {
            isIdle = false;
            _anim.SetTrigger("GS2");
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

    }

    public void FinishAbilityTwoGun()
    {
        _anim.SetTrigger("Idle");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints -= RigidbodyConstraints.FreezePosition;
        isIdle = true;
    }

    public void ColliderActivationGS2(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        if (animColliders[ColliderNumber].activeSelf)
            animColliders[ColliderNumber].SetActive(false);
        else animColliders[ColliderNumber].SetActive(true);
    }
}
