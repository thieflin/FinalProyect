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
        
        _hunter.anim.SetBool("IdleB", false);
        _hunter.anim.SetBool("PatrolB", false);

        //cUANDO Lo detecta le desfreezeo asi no rebota a lo tonto
        _hunter.rb.constraints = RigidbodyConstraints.FreezeAll;
        _hunter.rb.constraints -= RigidbodyConstraints.FreezePositionX;
        _hunter.rb.constraints -= RigidbodyConstraints.FreezePositionZ;
        _hunter.rb.constraints -= RigidbodyConstraints.FreezePositionY;
    }

    public void OnUpdate()
    {
        Pursuit();
        Debug.Log("on chase");
    }

    void Pursuit() //Aplico pursuit
    {
        //Esto hace que lo chasee
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;
        _hunter.transform.position += dir.normalized * _hunter.speed * 1.5f * Time.deltaTime;

        //Esto hace que lo mire al perseguirlo
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        //Si estoy en distancia de atacar, paso a atacar
        if (dir.magnitude < _hunter.attackDistance)
        {
            _hunter.anim.SetTrigger("Attack");
            _fsm.ChangeState(PlayerStatesEnum.Attack);
        }
        else if(dir.magnitude >= _hunter.loseTargetDistance)
        {
            _hunter.anim.SetTrigger("Idle");
            _fsm.ChangeState(PlayerStatesEnum.Patrol);
        }
           
            
    }


}
