using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDStateRanged : MonoBehaviour, IState
{
    StateMachine _fms;
    HunterRanged _hunter;


    public CDStateRanged(StateMachine fms, HunterRanged h)
    {
        _fms = fms;
        _hunter = h;
    }

    public void OnExit()
    {

    }

    public void OnStart()
    {
        _hunter.attackCd = Random.Range(3, 6);
    }

    public void OnUpdate()
    {
        Debug.Log("en cd");

        _hunter.attackCd -= Time.deltaTime;



        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;
        _hunter.transform.position += dir.normalized * _hunter.speed * .5f /*para que chasee mas rapdio*/ * Time.deltaTime;
        //Hace que mire al player mientras espera y espere en idle (esto podria ponerse una animacion de transicion tambien)
        RotationTowardsPlayer();

        if (_hunter.attackCd < 0)
        {
            CheckNextActionAfterCd();
        }

    }

    public void RotationTowardsPlayer()
    {
        //Lo miro mientras estoy en cd
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);
    }

    public void CheckNextActionAfterCd()
    {
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;

        //Si estoy en distancia de perderlo VUELVO A PATROL
        if (dir.magnitude > _hunter.loseTargetDistance)
        {
            Debug.Log("NO LO TENGO EN RANGO ME VOY A CASA");
            //Este trigger podria pasarlo por animation event me parece seria mejor
            _hunter.anim.SetTrigger("BackToPatrol");
            _fms.ChangeState(PlayerStatesEnum.Patrol);

        }
        //Si estoy en DISTANCIA de no perderlo TENGO OPCIONES
        //La uno es que este muy cerca por lo que hace stepback y ataca
        //La 2 esta en attack distance asi que directamente lo ataca
        //La 3 esta sobre el rango de ataque asi que chasea
        else if (dir.magnitude < _hunter.loseTargetDistance)
        {
            if (dir.magnitude < _hunter.stepbackDistance)
                _fms.ChangeState(PlayerStatesEnum.StepbackState);
            else if (dir.magnitude < _hunter.attackDistance)
            {
                _hunter.anim.SetTrigger("AttackAgain");
                _fms.ChangeState(PlayerStatesEnum.Attack);
            }
            else if (dir.magnitude < _hunter.detectDistance)
                _fms.ChangeState(PlayerStatesEnum.Chase);
        }
    }
}
