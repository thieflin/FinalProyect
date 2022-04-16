using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Abilities : MonoBehaviour
{
    protected int requiredPowerGauge;
    public bool isActive;
    public bool isOnUse;
    protected bool isIdle;
    protected int _cost;
    public CharStatus _cs;
    public Animator _anim;
    public GameObject actionCollider;
    public Image abilityIcon;
    public int abilityId;

    public bool isPurchased;

    public virtual void OnUpdate()
    {

    }
    public virtual void Attack()
    {
        //Attack
    }
}
