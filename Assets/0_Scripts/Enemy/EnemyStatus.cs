using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyData, IDamageable
{

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
            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            gameObject.SetActive(false);

            if (isWaveEnemy)
            {
                EventManager.Instance.Trigger("OnKillingWaveEnemy");
            }

            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO M�S
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
        }

        if(other.gameObject.layer == _abilityLayermask && !other.CompareTag("Bullet"))
        {
            TakeDamage(100);
        }
    }

}
