using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeleeOne : Abilities
{

    private void Start()
    {
        _cs = GetComponent<CharStatus>();
        id = 0;
    }

    public override void OnUpdate()
    {
        Attack();

    }
    public override void Attack()
    {
        Debug.Log("Melee1");

        if (isIdle == true)
        {
            //Que la pueda cstear por deporte por ahora
            //_cs.UseAbility(id);
            isIdle = false;
            _anim.SetTrigger("MS1");
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        
    }

    //Event animation ultimo frame para raetornar a idle
    public void FinishAbilityOneMelee()
    {
        _anim.SetTrigger("Idle");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints -= RigidbodyConstraints.FreezePosition;
        isIdle = true;
    }

    //Aactvacion de collidersssss
    public void ColliderActivationMS1(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        if (animColliders[ColliderNumber].activeSelf)
            animColliders[ColliderNumber].SetActive(false);
        else animColliders[ColliderNumber].SetActive(true);
    }
}
