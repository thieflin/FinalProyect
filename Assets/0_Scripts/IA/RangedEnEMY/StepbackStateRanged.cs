using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepbackStateRanged : MonoBehaviour, IState
{
    HunterRanged _hunter;
    StateMachine _fsm;


    public GameObject target;

    private Vector3 _velocity;


    
    public StepbackStateRanged(StateMachine fsm, HunterRanged h)
    {
        _fsm = fsm;
        _hunter = h;
    }

    public void OnExit()
    {
        

    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Stepback");
    }

    public void OnUpdate()
    {
        Debug.Log("on chase");
    }

}
