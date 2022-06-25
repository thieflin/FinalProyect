using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStateRanged : MonoBehaviour, IState
{


    StateMachine _fms;
    HunterRanged _hunter;


    public HitStateRanged(StateMachine fms, HunterRanged h)
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
