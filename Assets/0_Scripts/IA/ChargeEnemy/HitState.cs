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
    }

    public void OnUpdate()
    {
        
    }

    public void SmallKnockBack()
    {
        //_hunter.rb.AddForce(_hunter.transform.forward * )
    }
}
