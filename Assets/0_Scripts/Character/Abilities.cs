using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    protected int requiredPowerGauge;
    public bool isActive;
    protected bool isIdle;
    protected int _cost;
    public CharStatus _cs;
    public Animator _anim;
    public GameObject actionCollider;

    public virtual void Attack()
    {
        //Attack
    }

    public virtual void FinishAbility()
    {
        //Terminar la habilidad (animation related)
    }

    public virtual void ActivateColliderAndAnim()
    {
        //Activa collider
    }

    public virtual void DeactivateCollider()
    {
        //Desactiva collider
    }
}
