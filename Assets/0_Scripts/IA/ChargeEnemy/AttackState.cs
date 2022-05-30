using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour, IState
{


    StateMachine _fms;
    Hunter _hunter;


    public AttackState(StateMachine fms, Hunter h)
    {
        _fms = fms;
        _hunter = h;
    }



    public void OnExit()
    {
        _hunter.anim.SetTrigger("Idle");

    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Attack");
    }

    public void OnUpdate()
    {
        AttackAnimation();
        Debug.Log(_hunter.attackForces);
    }

    public void AttackAnimation()
    {
        _hunter.rb.AddForce(_hunter.transform.forward * _hunter.attackForces * Time.deltaTime, ForceMode.Impulse);
    }
}
