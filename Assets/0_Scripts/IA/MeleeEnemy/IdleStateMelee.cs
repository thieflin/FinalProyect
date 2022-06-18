using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateMelee : MonoBehaviour, IState
{


    StateMachine _fms;
    HunterMelee _hunter;

    public IdleStateMelee(StateMachine fms, HunterMelee h)
    {
        _fms = fms;
        _hunter = h;
    }

    private float idleHold;


    public void OnExit()
    {
        _hunter.anim.SetBool("IdleB", false);
    }

    public void OnStart()
    {
        _hunter.anim.SetBool("IdleB", true);
        idleHold = 3;
    }

    public void OnUpdate()
    {
        Debug.Log("en idle");
        idleHold -= Time.deltaTime;

        //Busco el vector al cual yo quiero rotar es decir donde llegue ahi freno
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.allWaypoints[_hunter.currentWaypoint].transform.position);

        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        //Cuando llega ahi deja de rotar
        if(_hunter.transform.rotation == Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime) || idleHold < 0)
            _fms.ChangeState(PlayerStatesEnum.Patrol);
    }

    public void DetectEnemy()
    {
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;

        if (dir.magnitude < _hunter.detectDistance)
            _fms.ChangeState(PlayerStatesEnum.DetectionState);
    }
}
