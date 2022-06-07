using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterRanged : MonoBehaviour
{
    [Header("Movement")]
    public int speed;//Velocidad de movimiento
    public int currentWaypoint; //Al waypoint al que voy
    public int wpCounter; //Contador de wayPoints
    public List<Transform> allWaypoints = new List<Transform>(); //Lista de waypoints en los cuales se va a mover la IA
    public int waypointTracker; //Cantidad de waypoints por los que quiero que pase

    [Header("Idle")]
    public float rotationSpeedOnIdle; //Que tan rapido rota en el idle
    public float waitForNextAttack;

    [Header("Target & Others")]
    public PlayerMovement target; //El player
    public float attackDistance; //Attack distance es para poder hacer que se vaya hacia atras
    public float detectDistance; //Distancia para que lo detecte y lo persiga
    public float loseTargetDistance; //Distancia a lo cual lo pierde y vuelve a lo suyo
    public int attackForces; //Cuanta fuerza tiene el movimiento con el que se desplaza
    public int knockbackForce; //Fuerza del knock back
    public int layerHit; //Layer de colision
    public bool isTargetting; //Variable para que cuadno carga el ataque me mire
    public List<GameObject> hitColliders = new List<GameObject>(); //Colliders de las garras
    public int damageDone;//Da;o que hace al player
    public bool justAttacked; //Booleano para saber si le pego al player

    [Header("Cosmetics")]
    public GameObject particleSystemXD;

    public Rigidbody rb;
    public Animator anim;



    private StateMachine _fsm;


    void Start()
    {
        _fsm = new StateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        _fsm.AddState(PlayerStatesEnum.Patrol, new WaypointStateRanged(_fsm, this)); //Agrego todos los estados
        _fsm.AddState(PlayerStatesEnum.Idle, new IdleStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Chase, new ChaseStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Attack, new AttackStateRanged(_fsm, this));
        _fsm.ChangeState(PlayerStatesEnum.Patrol); //Lo hago arrancar con Idle
        _fsm.OnStart(); //Starteo la FSM

    }

    void Update()
    {
        _fsm.OnUpdate();
        
    }

    //Esta funcion hace que despues de atacar vuelva por defecto a Chasear, de ahi sale
    public void BackToIdle()
    {
        isTargetting = false;
        _fsm.ChangeState(PlayerStatesEnum.Chase);
    }

    //Esta funcion hace que se activen los collider trigger de las garras
    public void ActivateParticleAttackCharge()
    {
        particleSystemXD.SetActive(true);
    }

    public void DeactivateParticleAttackCharge()
    {
        particleSystemXD.SetActive(false);
    }

    //Esta funcion modifica la fuerza en la animacion con Animation Events
    public void BackwardsJump(int pushForce)
    {
        if (!justAttacked)
            attackForces = pushForce;
    }


    //Colisiones
    private void OnTriggerEnter(Collider other)
    {



    }

}
