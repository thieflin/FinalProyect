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
        if (_currentHp <= 0)
            gameObject.SetActive(false);
            //Estaria bueno hacer un pool de enemigos e irlos spawneando cada tanto
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _hitboxLayermask)
        {
            Debug.Log("toy pegando jeje");
            TakeDamage(Combo.swordDmg);
        }
    }
}
