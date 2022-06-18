using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepbackStateRanged : MonoBehaviour, IState
{
    HunterRanged _hunter;
    StateMachine _fsm;


    public GameObject target;

    private Vector3 _velocity;


    
    public StepbackStateRanged(StateMachine fsm, HunterRanged h)
    {
        _fsm = fsm;
        _hunter = h;
    }

    public void OnExit()
    {

    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Stepback");
    }

    public void OnUpdate()
    {
        LookAtPlayer();
        StepbackAnimation();
    }

    public void LookAtPlayer()
    {
        //Esto hace que lo mire al perseguirlo
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);
    }


    //Esta funcion hace que agarre fuerza en el rigid body para saltar hacia atras, esto salta CUANDO
    public void StepbackAnimation()
    {
        _hunter.rb.AddForce(_hunter.transform.forward * _hunter.attackForces * Time.deltaTime, ForceMode.Impulse);
    }
}
