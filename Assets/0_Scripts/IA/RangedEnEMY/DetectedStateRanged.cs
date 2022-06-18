using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedStateRanged : MonoBehaviour
{
    StateMachine _fms;
    HunterMelee _hunter;


    public DetectedStateRanged(StateMachine fms, HunterMelee h)
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
        CourseOfAction();
    }

    public void RotationTowardsPlayer()
    {
        //Esto hace que lo mire al perseguirlo
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);
    }

    public void CourseOfAction()
    {
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;
        _hunter.transform.position += dir.normalized * _hunter.speed * .5f /*para que chasee mas rapdio*/ * Time.deltaTime;


    }
}
