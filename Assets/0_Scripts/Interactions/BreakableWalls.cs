using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWalls : Objects, IDamageable
{
    public void TakeDamage(int dmg)
    {
        _maxHits -= dmg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _hitboxLayermask)
        {
            TakeDamage(_hitCost);
            if(_maxHits <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
