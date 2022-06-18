using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedStateRanged : MonoBehaviour, IState
{
    StateMachine _fms;
    HunterRanged _hunter;


    public DetectedStateRanged(StateMachine fms, HunterRanged h)
    {
        _fms = fms;
        _hunter = h;
    }

    public void OnExit()
    {
        Debug.Log("sali de deteccion");
    }

    public void OnStart()
    {
        //Aca al final de esta animacion entra una funcion de trigger que hace que pase a el estado de chase
        _hunter.anim.SetTrigger("Detected");
        _hunter.anim.SetBool("PatrolB", false);
        _hunter.anim.SetBool("IdleB", false);
    }

    public void OnUpdate()
    {
        RotationTowardsPlayer();
    }

    public void RotationTowardsPlayer()
    {
        //Esto hace que lo mire al perseguirlo
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);
    }

}
