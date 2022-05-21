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
        _currentHp -= (int)(dmg * _dmgMitigation);

        AudioManager.PlaySound("hit");

        _anim.SetTrigger("Hit");

        if (_currentHp <= 0)
        {
            if (isWaveEnemy)
            {
                EventManager.Instance.Trigger("OnKillingWaveEnemy");
            }
            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO MÁS
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
        if (other.gameObject.layer == _hitboxLayermask && !other.CompareTag("Bullet"))
        {
            Debug.Log("toy pegando jeje");
            TakeDamage(Combo.swordDmg);
            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);
            //no

        }

        if (other.gameObject.layer == _abilityLayermask && !other.CompareTag("Bullet"))
        {
            TakeDamage(100);
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
