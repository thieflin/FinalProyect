using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [Header("Movement")]
    public int speed;//Velocidad de movimiento
    public int currentWaypoint; //Al waypoint al que voy
    public int wpCounter; //Contador de wayPoints
    public List<Transform> allWaypoints = new List<Transform>(); //Lista de waypoints en los cuales se va a mover la IA

    [Header("Idle")]
    public bool isThinking;
    public float thinkingmeter;
    public float rotationSpeedOnIdle;

    [Header("Target & Others")]
    public PlayerMovement target; //El player
    public float attackDistance;
    public float detectDistance;
    public float loseTargetDistance;
    public int walkBackForce;

    public Rigidbody rb;
    public Animator anim;



    private StateMachine _fsm;


    void Start()
    {
        _fsm = new StateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        _fsm.AddState(PlayerStatesEnum.Patrol, new WaypointState(_fsm, this)); //Agrego todos los estados
        _fsm.AddState(PlayerStatesEnum.Idle, new IdleState(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Chase, new ChaseState(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Attack, new AttackState(_fsm, this));
        _fsm.ChangeState(PlayerStatesEnum.Patrol); //Lo hago arrancar con Idle
        _fsm.OnStart(); //Starteo la FSM

    }

    void Update()
    {
        _fsm.OnUpdate();
        
    }

    //Esta funcion actualiza la FUERZA PARA EL ATAQUE, es una funcion de animaciones

    public void AttackIsFinished()
    {
        _fsm.ChangeState(PlayerStatesEnum.Idle);
    }

    public void MovementInAttack(int pushForce)
    {
        walkBackForce = pushForce;
    }

}
