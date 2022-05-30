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
        _hunter.anim.SetBool("HitB", false);
    }

    public void OnStart()
    {
        _hunter.anim.SetBool("HitB", true);
        Debug.Log("entre hit");
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
