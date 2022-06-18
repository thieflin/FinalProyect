using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDLECDStateMelee : MonoBehaviour, IState
{
    StateMachine _fms;
    HunterMelee _hunter;


    public IDLECDStateMelee(StateMachine fms, HunterMelee h)
    {
        _fms = fms;
        _hunter = h;
    }

    public void OnExit()
    {
        _hunter.anim.SetTrigger("Patrol");
    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Idle");
        _hunter.idleWpCd = Random.Range(4,8);
    }

    public void OnUpdate()
    {
        Debug.Log("en idle cd");

        _hunter.idleWpCd -= Time.deltaTime;
        if (_hunter.idleWpCd < 0)
        {
            _fms.ChangeState(PlayerStatesEnum.Idle);
        }


        CheckNextActionAfterCd();

    }


    public void CheckNextActionAfterCd()
    {
        //Trazo un vector del player al jugador
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;

        //Si estoy en posicion de ataque lo ataco
        if (dir.magnitude < _hunter.attackDistance)
            _fms.ChangeState(PlayerStatesEnum.Attack);
        //Si estoy en posicion de detectarlo lo chaseo
        else if (dir.magnitude < _hunter.detectDistance)
            _fms.ChangeState(PlayerStatesEnum.DetectionState);
    }
}
