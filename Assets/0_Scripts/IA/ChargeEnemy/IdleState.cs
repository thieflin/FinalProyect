using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{


    StateMachine _fms;
    Hunter _hunter;
    bool isResting;

    public IdleState(StateMachine fms, Hunter h)
    {
        _fms = fms;
        _hunter = h;
    }



    public void OnExit()
    {
        _hunter.anim.SetBool("IdleB", false);
    }

    public void OnStart()
    {
        _hunter.anim.SetBool("IdleB", true);
    }

    public void OnUpdate()
    {
        //Busco el vector al cual yo quiero rotar es decir donde llegue ahi freno
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.allWaypoints[_hunter.currentWaypoint].transform.position);

        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        //Cuando llega ahi deja de rotar
        if(_hunter.transform.rotation == Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime))
            _fms.ChangeState(PlayerStatesEnum.Patrol);
    }

    public void DetectEnemy()
    {
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;

        if (dir.magnitude < _hunter.detectDistance)
            _fms.ChangeState(PlayerStatesEnum.Chase);
    }
}
