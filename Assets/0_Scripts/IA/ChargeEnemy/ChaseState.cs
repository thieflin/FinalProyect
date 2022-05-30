using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{


    Hunter _hunter;
    StateMachine _fsm;


    public GameObject target;

    private Vector3 _velocity;

    

    public ChaseState(StateMachine fsm, Hunter h)
    {
        _fsm = fsm;
        _hunter = h;
    }

    public void OnExit()
    {
        
    }

    public void OnStart()
    {
        
    }

    public void OnUpdate()
    {
        //_hunter.staminaBar -= Time.deltaTime; //Perdida de Stamina, cuadno chasea pierde una cantidad

        //if (_hunter.staminaBar <= 0) //Si chaseando se queda en 0, se cambia a Idle
        //    _fsm.ChangeState(PlayerStatesEnum.Idle);


        //_hunter.transform.position += _velocity * Time.deltaTime;
        //_hunter.transform.forward = _velocity;
        //Pursuit();

        Pursuit();
        Debug.Log("on chase");
    }

    void Pursuit() //Aplico pursuit
    {
        //Esto hace que lo chasee
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;
        _hunter.transform.position += dir.normalized * _hunter.speed * Time.deltaTime;

        //Esto hace que lo mire al perseguirlo
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        //Si estoy en distancia de atacar, paso a atacar
        if (dir.magnitude < _hunter.attackDistance)
        {
            _fsm.ChangeState(PlayerStatesEnum.Attack);
        }
        

               

    }




}
