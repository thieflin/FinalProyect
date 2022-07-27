using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGunOne : Abilities
{
    private void Start()
    {
        _cs = GetComponent<CharStatus>();
        id = 1;
    }



    public override void OnUpdate()
    {
        Attack();

    }

    //Esto es el ataque que usa
    public override void Attack()
    {
        Debug.Log("GunOne");

        if (isIdle == true)
        {
            //Que la pueda castear por deporte ahora nomas
            //_cs.UseAbility(id);
            isIdle = false;
            _anim.SetTrigger("GS1");
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

    }

    //Event animation al final de la animacion
    public void FinishAbilityOneGun()
    {
        _anim.SetTrigger("Idle");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints -= RigidbodyConstraints.FreezePosition;
        isIdle = true;
    }

    //Event animation para activar los colliders
    public void ColliderActivationGS1(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        if (animColliders[ColliderNumber].activeSelf)
            animColliders[ColliderNumber].SetActive(false);
        else animColliders[ColliderNumber].SetActive(true);
    }
}
