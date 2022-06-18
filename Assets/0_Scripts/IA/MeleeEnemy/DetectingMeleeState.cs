using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingMeleeState : MonoBehaviour, IState
{
    StateMachine _fms;
    HunterMelee _hunter;


    public DetectingMeleeState(StateMachine fms, HunterMelee h)
    {
        _fms = fms;
        _hunter = h;
    }

    public void OnExit()
    {
        Debug.Log("sali de deteccion");
    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Detected");
    }

    public void OnUpdate()
    {

        Debug.Log("entre en deteccion");

    }

    public void CheckNextActionAfterCd()
    {

    }
}
