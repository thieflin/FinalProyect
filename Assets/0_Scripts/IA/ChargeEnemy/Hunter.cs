using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [Header("Movement")]
    public int speed;//Velocidad de movimiento
    public int currentWaypoint;
    public int wpCounter;
    public List<Transform> allWaypoints = new List<Transform>(); //Lista de waypoints en los cuales se va a mover la IA



    private StateMachine _fsm;


    void Start()
    {
        _fsm = new StateMachine();

        _fsm.AddState(PlayerStatesEnum.Patrol, new WaypointState(_fsm, this)); //Agrego todos los estados
        _fsm.AddState(PlayerStatesEnum.Idle, new IdleState(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Chase, new ChaseState(_fsm, this));
        _fsm.ChangeState(PlayerStatesEnum.Patrol); //Lo hago arrancar con Idle
        _fsm.OnStart(); //Starteo la FSM

    }

    void Update()
    {
        _fsm.OnUpdate();
    }

    

}
