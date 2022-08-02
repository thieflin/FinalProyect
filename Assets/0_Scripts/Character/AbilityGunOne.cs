using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGunOne : Abilities
{

    public GameObject bulletSpawner;
    public GameObject bulletPrefab;
    public ParticleSystem shootParticle;
    public ParticleSystem shootParticle2;
    public ParticleSystem shootParticle3;


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
            _cs.UseAbility(id);
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

    //Event animation particle shooting
    public void ParticleRangedAbility()
    {
        AudioManager.PlaySound("ShotgunShort");
        shootParticle.Play();
        shootParticle2.Play();
        shootParticle3.Play();
    }


    //Event animation shooting
    public void IsntantiateBulletRangedAbility()
    {
        var instantiateBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, bulletSpawner.transform.rotation);
    }




    //Event animation para activar los colliders
    public void ColliderActivationGS1(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        if (animColliders[ColliderNumber].activeSelf)
            animColliders[ColliderNumber].SetActive(false);
        else animColliders[ColliderNumber].SetActive(true);
    }
}
