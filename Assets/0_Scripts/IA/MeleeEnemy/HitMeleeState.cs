using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMeleeState : MonoBehaviour, IState
{

    StateMachine _fms;
    HunterMelee _hunter;


    public HitMeleeState(StateMachine fms, HunterMelee h)
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
        _hunter.anim.SetBool("IdleB", false);
    }

    public void OnUpdate()
    {
        Debug.Log("me stunearon");
    }
}
