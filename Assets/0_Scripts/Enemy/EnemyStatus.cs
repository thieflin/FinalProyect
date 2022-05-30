using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyData, IDamageable
{

    public GameObject onMeleeHittedParticles;
    public bool enemyHitted = false;

    public void Start()
    {
        _currentHp = _maxHp;
        _anim = GetComponent<Animator>();
        startPos = transform.position;

    }
    public void TakeDamage(int dmg)
    {
        //Le saco vida
        _currentHp -= dmg;


        if (_currentHp <= 0)
        {
            if (isWaveEnemy)
            {
                EventManager.Instance.Trigger("OnKillingWaveEnemy");
            }


            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO M�S
            if (TargetLock.enemiesClose.Contains(this.GetComponent<Enemy>()))
            {
                TargetLock.enemiesClose.Remove(this.GetComponent<Enemy>());
            }

            gameObject.SetActive(false);
        }
        //Estaria bueno hacer un pool de enemigos e irlos spawneando cada tanto
    }

    public void KnockBack()
    {
        _rb.AddForce(transform.forward * Time.deltaTime * -knockBackForce, ForceMode.Force);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Cuando le pego con los ataques normales
        if (other.gameObject.layer == _hitboxLayermask && !other.CompareTag("Bullet"))
        {
            //Recibe dmg de lo que sea que valga el sword dmg, esto lo defino en combo asi siempre vale lo que yo quiero
            TakeDamage(Combo.swordDmg);

            //Instancia particulas
            //Ver de cambiar para que cada enemigo tenga su particula adentro y no la este INSTANCIANDO cada vez que le pego
            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);

        }

        //Si le pego con el ultimo hit, el que knockbackea PIOLI
        else if (other.gameObject.layer == _hitKnockbackLayerMask)
        {
            //Recibe dmg
            TakeDamage(Combo.swordDmg);


            //Ver de cambiar para que cada enemigo tenga su particula adentro y no la este INSTANCIANDO cada vez que le pego
            //Instancia las partic
            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet") && !enemyHitted)
        {
            TakeDamage(10);

            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);
            if (this.gameObject.activeSelf)
                StartCoroutine(WaitForEnemyHitted());
        }
    }

    IEnumerator WaitForEnemyHitted()
    {
        enemyHitted = true;
        yield return new WaitForSeconds(0.3f);
        enemyHitted = false;
    }
}
