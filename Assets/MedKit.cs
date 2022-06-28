using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : Objects
{
    public int healAmount;
    public GameObject firstChild;
    bool used;

    private void Start()
    {
        if (transform.GetChild(0) != null)
            firstChild = transform.GetChild(0).gameObject;
    }


    public void TakeDamage(int dmg)
    {
        _maxHits -= dmg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _hitboxLayermask && !used)
        {
            TakeDamage(_hitCost);

            if (_maxHits <= 0)
            {
                var charStatus = other.transform.parent.GetComponent<CharStatus>();
                charStatus.hp += healAmount;
                if (charStatus.hp >= charStatus.maxHp)
                {
                    charStatus.hp = charStatus.maxHp;
                }

                charStatus._hpBar.fillAmount = charStatus.HpPercentCalculation(charStatus.hp);

                used = true;

                firstChild.SetActive(false);
            }
        }
    }
}
