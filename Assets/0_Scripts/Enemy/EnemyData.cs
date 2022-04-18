using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyData : MonoBehaviour
{
    [SerializeField] protected int _currentHp;
    [SerializeField] protected int _maxHp;
    [SerializeField] protected int _hitboxLayermask, _abilityLayermask;
    [SerializeField] protected float _dmgMitigation;
    [SerializeField] protected Slider _hpSlider;
    [SerializeField] protected float _expPoints;


    public virtual void GetEXPPoints(float expPoints)
    {
        EventManager.Instance.Trigger("OnGettingExp", expPoints);
    }

    public int GetHP()
    {
        return _currentHp;
    }

    public int GetMaxHP()
    {
        return _maxHp;
    }

}
