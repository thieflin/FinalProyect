using System.Collections.Generic;
using UnityEngine;

public class WaypointState : MonoBehaviour, IState
{
    
    Hunter _hunter;
    StateMachine _fsm;

    //Este script se va a encargar de mover al enemigo entre los distintos spots

    public WaypointState(StateMachine fsm, Hunter h) //Constreuctor state machine
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
    
    public void OnUpdate() //OnUpdate arranco con esto
    {

        Move();
    }

    public void Move() //Funcion de movimiento de waypoints
    {

        Vector3 dir = _hunter.allWaypoints[_hunter.currentWaypoint].transform.position - _hunter.transform.position;
        transform.forward = dir;

        transform.position += transform.forward * _hunter.speed * Time.deltaTime;

        if (dir.magnitude < 0.1f)
        {
            
            var lastWp = _hunter.currentWaypoint;
            //Le digo que elija uno al azar de los 9
            _hunter.currentWaypoint = Random.Range(0, _hunter.allWaypoints.Count - 1);
            _hunter.wpCounter++;
            //Si eligio el mismo, entonces le digo que elija a otro
            if (_hunter.currentWaypoint == lastWp)
                while (_hunter.currentWaypoint == lastWp)
                    _hunter.currentWaypoint = Random.Range(0, _hunter.allWaypoints.Count - 1);
        }


        if (_hunter.wpCounter == 4)
        {

            _hunter.currentWaypoint = 0;
            _hunter.wpCounter = 0;
        }

    }


}
