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
    public float rotationSpeedOnIdle; //Que tan rapido rota en el idle

    [Header("Target & Others")]
    public PlayerMovement target; //El player
    public float attackDistance; //Distancia para que ejecute el ataque
    public float detectDistance; //Distancia para que lo detecte y lo persiga
    public float loseTargetDistance; //Distancia a lo cual lo pierde y vuelve a lo suyo
    public int attackForces; //Cuanta fuerza tiene el movimiento con el que se desplaza
    public int knockbackForce; //Fuerza del knock back
    public int layerHit; //Layer de colision
    public bool isTargetting; //Variable para que cuadno carga el ataque me mire

    [Header("Cosmetics")]
    public List<GameObject> clawAttackParticles = new List<GameObject>();

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
        _fsm.AddState(PlayerStatesEnum.Hit, new HitState(_fsm, this));
        _fsm.ChangeState(PlayerStatesEnum.Patrol); //Lo hago arrancar con Idle
        _fsm.OnStart(); //Starteo la FSM

    }

    void Update()
    {
        _fsm.OnUpdate();
        
    }

    public void TargetEnemyOnPreparation()
    {
        if (!isTargetting) isTargetting = true;
        else if (isTargetting) isTargetting = false;
    }


    //Esta funcion hace que despues de atacar vuelva por defecto a idle
    public void BackToIdle()
    {
        _fsm.ChangeState(PlayerStatesEnum.Chase);
    }

    //Esta funcion modifica la fuerza en la animacion con Animation Events
    public void MovementInAttack(int pushForce)
    {
        attackForces = pushForce;
    }

    //Cuando le pegan cambia el estado
    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.layer == layerHit)
        //{
        //    //_fsm.ChangeState(PlayerStatesEnum.Hit);
        //    //anim.SetTrigger("Hit");
        //}
           
    }

}
