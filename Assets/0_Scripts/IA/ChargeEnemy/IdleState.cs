using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{




    StateMachine _fms;
    Hunter _hunter;

    public IdleState(StateMachine fms, Hunter h)
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
        _hunter.idleWpCd = Random.Range(4, 8);
    }

    public void OnUpdate()
    {
        //Basicmaente cada vez que entro aca lo que hace es esperar un cierto tiempo random antes de moverse
        _hunter.idleWpCd -= Time.deltaTime;
        if (_hunter.idleWpCd < 0)
        {
            //Una vez que el idleWpCd esta en 0, busca el proximo waypoint y sigue hacia el con el codigo que usaba antes.
            LeaveIdleAfterCD();
        }

        //Siempre uso detect enemy para ir al estado de DETECCION en caso de que si esta en idle lo busque y haga la animacion
        //de que lo encontro
        if (_hunter.detectionBox.playerInBox)
            DetectEnemy();
        Debug.Log("estoy en idle");
    }

    public void DetectEnemy()
    {
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;

        if (dir.magnitude < _hunter.detectDistance)
            _fms.ChangeState(PlayerStatesEnum.Chase);
    }

    public void LeaveIdleAfterCD()
    {
        //Busco el vector al cual yo quiero rotar es decir donde llegue ahi freno
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.allWaypoints[_hunter.currentWaypoint].transform.position);

        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        //Cuando llega ahi deja de rotar
        if (_hunter.transform.rotation == Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime) || idleHold < 0)
            _fms.ChangeState(PlayerStatesEnum.Patrol);
    }
}
