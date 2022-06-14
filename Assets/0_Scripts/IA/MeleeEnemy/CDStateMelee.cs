using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDStateMelee : MonoBehaviour, IState
{
    StateMachine _fms;
    HunterMelee _hunter;


    public CDStateMelee(StateMachine fms, HunterMelee h)
    {
        _fms = fms;
        _hunter = h;
    }

    public void OnExit()
    {
        _hunter.attackCd = 4f;
        _hunter.anim.SetTrigger("Patrol");
    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Idle");
        _hunter.attackCd = 4f;
    }

    public void OnUpdate()
    {
        Debug.Log("en cd");

        _hunter.attackCd -= Time.deltaTime;

        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;
        _hunter.transform.position += dir.normalized * _hunter.speed * .5f /*para que chasee mas rapdio*/ * Time.deltaTime;
        //Hace que mire al player mientras espera y espere en idle (esto podria ponerse una animacion de transicion tambien)
        RotationTowardsPlayer();


        if(_hunter.attackCd < 0)
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


        //Si estoy en posicion de ataque lo ataco
        if (dir.magnitude < _hunter.attackDistance)
            _fms.ChangeState(PlayerStatesEnum.Attack);
        //Si estoy en posicion de detectarlo lo chaseo
        else if (dir.magnitude < _hunter.detectDistance)
            _fms.ChangeState(PlayerStatesEnum.Chase);
        else _fms.ChangeState(PlayerStatesEnum.Chase);
    }
}
