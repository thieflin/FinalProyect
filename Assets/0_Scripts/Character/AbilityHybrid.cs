using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHybrid : Abilities
{

    public ParticleSystem slash1, slash2, slash3, slash4;
    public ParticleSystem shootingParticle;
    public ParticleSystem shootingParticle2;
    public GameObject bulletSpawner;
    public GameObject bulletPrefab;

    public GameObject collider1, collider2, collider3, collider4;



    private void Start()
    {
        _cs = GetComponent<CharStatus>();
        id = 2; //Esto la define como hibrida
    }

    public override void OnUpdate()
    {
        Attack();

    }
    public override void Attack()
    {
        Debug.Log("Hybrid");


            //Que la pueda cstear por deporte por ahora
            _cs.UseAbility(id);
            isIdle = false;
            Debug.Log("sifuncionaura");
            _anim.SetTrigger("HS1");
            rb.constraints = RigidbodyConstraints.FreezeAll;
        

    }

    //Event animation ultimo frame para raetornar a idle
    public void FinishAbilityOneHybrid()
    {
        _anim.SetTrigger("Idle");
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints -= RigidbodyConstraints.FreezePosition;
        isIdle = true;
    }

    //Activacion de todas las particulas

    public void ParticleActivationSlash1HS1()
    {
        slash1.Play();
        collider1.SetActive(true);
    }


    public void ParticleActivationSlash2HS1()
    {
        slash2.Play();
        collider2.SetActive(true);
    }

    public void ParticleActivationSlash3HS1()
    {
        slash3.Play();
        slash4.Play();
        collider3.SetActive(true);
        collider4.SetActive(true);
    }

    public void ColliderDeactivationSlash1HS1()
    {
        collider1.SetActive(false);

    }
    public void ColliderDeactivationSlash2HS1()
    {
        collider2.SetActive(false);

    }
    public void ColliderDeactivationSlash3HS1()
    {
        collider3.SetActive(false);
        collider4.SetActive(false);

    }


    public void ParticleActivationShoot()
    {
        shootingParticle.Play();
        AudioManager.PlaySound("ShotgunShort");
    }
    public void ParticleActivation3rdShot()
    {
        shootingParticle2.Play();
        AudioManager.PlaySound("ShotgunShort");
    }

    public void IsntantiateBulletHybridAbility()
    {
        var instantiateBullet = Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation);
    }





    //Aactvacion de collidersssss
    public void ColliderActivationHS1(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        if (animColliders[ColliderNumber].activeSelf)
            animColliders[ColliderNumber].SetActive(false);
        else animColliders[ColliderNumber].SetActive(true);
    }
}
