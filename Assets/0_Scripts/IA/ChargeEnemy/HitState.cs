using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : MonoBehaviour, IState
{

    
    StateMachine _fms;
    Hunter _hunter;

    public HitState(StateMachine fms, Hunter h)
    {
        _fms = fms;
        _hunter = h;
    }

    

    public void OnExit()
    {

    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Hit");
        _hunter.anim.SetBool("PatrolB", false);
        _hunter.anim.SetBool("IdleB", true);
        for (int i = 0; i < _hunter.clawAttackParticles.Count; i++)
        {
            _hunter.clawAttackParticles[i].SetActive(false);
        }
    }

    public void OnUpdate()
    {
        SmallKnockBack();
        Debug.Log("sigo hit state");

    }

    public void SmallKnockBack()
    {
        _hunter.rb.AddForce(_hunter.transform.forward * _hunter.knockbackForce * Time.deltaTime, ForceMode.Impulse);
    }
}
