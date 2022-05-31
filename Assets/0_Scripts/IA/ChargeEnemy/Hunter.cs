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
    public int waypointTracker; //Cantidad de waypoints por los que quiero que pase

    [Header("Idle")]
    public float rotationSpeedOnIdle; //Que tan rapido rota en el idle
    public float waitForNextAttack;

    [Header("Target & Others")]
    public PlayerMovement target; //El player
    public float attackDistance; //Distancia para que ejecute el ataque
    public float detectDistance; //Distancia para que lo detecte y lo persiga
    public float loseTargetDistance; //Distancia a lo cual lo pierde y vuelve a lo suyo
    public int attackForces; //Cuanta fuerza tiene el movimiento con el que se desplaza
    public int knockbackForce; //Fuerza del knock back
    public int layerHit; //Layer de colision
    public bool isTargetting; //Variable para que cuadno carga el ataque me mire
    public List<GameObject> hitColliders = new List<GameObject>(); //Colliders de las garras
    public int damageDone;//Da;o que hace al player
    public bool justAttacked;

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

    //Funcion de animation event, se encarga de hacer que triggere el booleano que le permite trackear al jugador antes de saltar
    public void TargetEnemyOnPreparation()
    {
        if (!isTargetting) isTargetting = true;
        else if (isTargetting) isTargetting = false;
    }

    public void ActivateClawParticles()
    {
        foreach (var item in clawAttackParticles)
        {
            if (!item.activeSelf) item.SetActive(true);
            else item.SetActive(false);
        }
    }

    //Esta funcion hace que despues de atacar vuelva por defecto a Chasear, de ahi sale
    public void BackToIdle()
    {
        isTargetting = false;
        _fsm.ChangeState(PlayerStatesEnum.Chase);
    }

    //Esta funcion modifica la fuerza en la animacion con Animation Events
    public void MovementInAttack(int pushForce)
    {
        attackForces = pushForce;
    }

    //Esta funcion hace que se activen los collider trigger de las garras
    public void ActivateColliderTriggerClaws()
    {
        //Activacion y desactivacion de colliders, esto podria hacerlo un collider singular despues si quisiera
        foreach (var item in hitColliders)
        {
            if (!item.GetComponent<BoxCollider>().enabled) item.GetComponent<BoxCollider>().enabled = true;
            else item.GetComponent<BoxCollider>().enabled = false;
        }
    }

    //Colisiones
    private void OnTriggerEnter(Collider other)
    {

        
        var player = other.GetComponent<CharStatus>();

        //Si colision con algo de tipo PLAYER, entonces le hago el dmg que este bicho haga a player
        if (player)
            player.TakeDamage(damageDone);

        var sword = other.GetComponent<ColliderPG>();

        //Hit SOLO transiciona si estoy CARGANDO EL ATAQUE, sino no lo stunea sino no funciona ni pal pingovich
        if (sword && isTargetting)
        {
            foreach (var item in clawAttackParticles)
            {
                item.SetActive(false);
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reciving_PEnemyTopo"))
            {

                Debug.Log("Toy ahciendo hitttt");
            }
            else anim.SetTrigger("Hit");

            _fsm.ChangeState(PlayerStatesEnum.Chase);
        }


    }

}
