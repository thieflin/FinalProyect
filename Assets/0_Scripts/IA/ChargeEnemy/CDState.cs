using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDState : MonoBehaviour, IState
{
    StateMachine _fms;
    Hunter _hunter;


    public CDState(StateMachine fms, Hunter h)
    {
        _fms = fms;
        _hunter = h;
    }

    public void OnExit()
    {
        _hunter.attackCd = 4f;
    }

    public void OnStart()
    {
        _hunter.attackCd = 4f;
        _hunter.rb.constraints = RigidbodyConstraints.FreezeAll;
        _hunter.rb.constraints -= RigidbodyConstraints.FreezePositionX;
        _hunter.rb.constraints -= RigidbodyConstraints.FreezePositionZ;
        _hunter.rb.constraints -= RigidbodyConstraints.FreezePositionY;
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
        //Trazo un vector del player al jugador
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;


        ////Si estoy en posicion de ataque lo ataco
        //if (dir.magnitude < _hunter.attackDistance)
        //{
        //    _hunter.anim.SetTrigger("Attack");
        //    _fms.ChangeState(PlayerStatesEnum.Attack);
        //}

        if (dir.magnitude > _hunter.loseTargetDistance)
        {
            Debug.Log("NO LO TENGO EN RANGO ME VOY A CASA");
            //Este trigger podria pasarlo por animation event me parece seria mejor
            _hunter.anim.SetTrigger("BackToPatrol");
            _fms.ChangeState(PlayerStatesEnum.Patrol);

        }
        else if (dir.magnitude < _hunter.loseTargetDistance)
        {
            if (dir.magnitude < _hunter.attackDistance)
            {
                Debug.Log("LO PUEDO SEGUIR PERSIGUIENDO");
                _hunter.anim.SetTrigger("AttackAgain");
                _fms.ChangeState(PlayerStatesEnum.Attack);
            }
            else
            {
                Debug.Log("LO PUEDO SEGUIR PERSIGUIENDO");
                _hunter.anim.SetTrigger("ChaseAgain");
                _fms.ChangeState(PlayerStatesEnum.Chase);
            }
        }
    }
}
