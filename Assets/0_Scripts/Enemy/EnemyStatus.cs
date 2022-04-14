using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyData, IDamageable
{

    public void Start()
    {
        _currentHp = _maxHp;
        _hpSlider.maxValue = _maxHp;
        _hpSlider.value = _currentHp;
    }
    public void TakeDamage(int dmg)
    {
        _currentHp -= (int)(dmg * _dmgMitigation);
        
        _hpSlider.value = _currentHp / _maxHp;



        if (_currentHp <= 0)
        {
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _currentHp -= 10;
            _hpSlider.value = _currentHp;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _hitboxLayermask)
        {
            Debug.Log("toy pegando jeje");
            TakeDamage(Combo.swordDmg);
        }
    }

}
