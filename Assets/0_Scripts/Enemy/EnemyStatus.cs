using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyData, IDamageable
{

    public ParticleSystem onMeleeHittedParticles;

    public void Start()
    {
        _currentHp = _maxHp;
    }
    public void TakeDamage(int dmg)
    {
        _currentHp -= (int)(dmg * _dmgMitigation);

        AudioManager.PlaySound("hit");


        if (_currentHp <= 0)
        {
            if (isWaveEnemy)
            {
                EventManager.Instance.Trigger("OnKillingWaveEnemy");
            }
            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            gameObject.SetActive(false);



            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO MÁS
            if (TargetLock.enemiesClose.Contains(this.GetComponent<Enemy>()))
            {
                TargetLock.enemiesClose.Remove(this.GetComponent<Enemy>());
            }
        }
        //Estaria bueno hacer un pool de enemigos e irlos spawneando cada tanto
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _hitboxLayermask && !other.CompareTag("Bullet"))
        {
            Debug.Log("toy pegando jeje");
            TakeDamage(Combo.swordDmg);
            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);
        }

        if(other.gameObject.layer == _abilityLayermask && !other.CompareTag("Bullet"))
        {
            TakeDamage(100);
        }
    }

}
