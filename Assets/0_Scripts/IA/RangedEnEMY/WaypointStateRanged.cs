using System.Collections.Generic;
using UnityEngine;

public class WaypointStateRanged : MonoBehaviour, IState
{

    HunterRanged _hunter;
    StateMachine _fsm;

    //Este script se va a encargar de mover al enemigo entre los distintos spots

    public WaypointStateRanged(StateMachine fsm, HunterRanged h) //Constreuctor state machine
    {
        _fsm = fsm;
        _hunter = h;
    }


    public void OnExit()
    {
        _hunter.anim.SetBool("PatrolB", false);
    }

    public void OnStart()
    {
        _hunter.anim.SetBool("PatrolB", true);

    }
    
    public void OnUpdate() //OnUpdate arranco con esto
    {
        Debug.Log("im on patrol");
        //Movimiento base
        Move();
        LookAtWps();
        //Detectar enemigo cambio el estado
        DetectEnemy();
    }

    public void Move() //Funcion de movimiento de waypoints
    {


        //Busco el vector al cual yo quiero rotar es decir donde llegue ahi freno
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.allWaypoints[_hunter.currentWaypoint].transform.position);

        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        Vector3 dir = _hunter.allWaypoints[_hunter.currentWaypoint].transform.position - _hunter.transform.position;
        _hunter.transform.position += dir.normalized * _hunter.speed * Time.deltaTime;
        
        if (dir.magnitude < 0.1f)
        {
            //Guarda el ultimo wp al que fui
            var lastWp = _hunter.currentWaypoint;
            //Le digo que elija uno al azar de los 9
            _hunter.currentWaypoint = Random.Range(0, _hunter.allWaypoints.Count - 1);
            //Sumo para saber cuantos wp va
            _hunter.wpCounter++;

            //Si eligio el mismo, entonces le digo que elija a otro
            if (_hunter.currentWaypoint == lastWp)
                while (_hunter.currentWaypoint == lastWp)
                    _hunter.currentWaypoint = Random.Range(0, _hunter.allWaypoints.Count - 1);
            _fsm.ChangeState(PlayerStatesEnum.Idle);
        }

        //Cuando llega a la cantidad maxima de wps que quiero que recorra
        if (_hunter.wpCounter == _hunter.waypointTracker)
        {

            _hunter.currentWaypoint = 0;
            _hunter.wpCounter = 0;
        }

        DetectEnemy();
    }

    public void DetectEnemy()
    {
        Vector3 dir = _hunter.target.transform.position - _hunter.transform.position;

        if (dir.magnitude < _hunter.detectDistance)
            _fsm.ChangeState(PlayerStatesEnum.DetectionState);
    }

    public void LookAtWps()
    {
        //Busco el vector al cual yo quiero rotar es decir donde llegue ahi freno
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.allWaypoints[_hunter.currentWaypoint].transform.position);

        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);
    }
}
