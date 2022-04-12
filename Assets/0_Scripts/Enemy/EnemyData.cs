using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyData : MonoBehaviour
{
    [SerializeField] protected int _currentHp;
    [SerializeField] protected int _maxHp;
    [SerializeField] protected int _hitboxLayermask;
    [SerializeField] protected float _dmgMitigation;
    [SerializeField] protected Slider _hpSlider;
}
