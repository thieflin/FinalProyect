using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMelee : MonoBehaviour
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
    public float idleWpCd;

    [Header("Target & Others")]
    public PlayerMovement target; //El player
    public float attackDistance; //Attack distance es para poder hacer que se vaya hacia atras
    public float detectDistance; //Distancia para que lo detecte y lo persiga
    public float loseTargetDistance; //Distancia a lo cual lo pierde y vuelve a lo suyo
    public int attackForces; //Cuanta fuerza tiene el movimiento con el que se desplaza
    public int knockbackForce; //Fuerza del knock back
    public int layerHit; //Layer de colision
    public bool isTargetting; //Variable para que cuadno carga el ataque me mire

    public GameObject hitCollider; //Colliders para cuadno pega


    public int damageDone;//Da;o que hace al player
    public bool justAttacked; //Booleano para saber si le pego al player
    public float attackCd;//Cd despues de atacar
    public bool enemyHitted;//Esto es para saber que entro, lo hago por separado a enemy status para que sea mas comodo

    [Header("Cosmetics")]
    public GameObject particleSystemXD;

    public Rigidbody rb;
    public Animator anim;
    public EnemyStatus es;

    public DetectionBox detectionBox;

    public StateMachine _fsm;


    void Start()
    {
        _fsm = new StateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        es = GetComponent<EnemyStatus>();

        _fsm.AddState(PlayerStatesEnum.Patrol, new WaypointStateMelee(_fsm, this)); //Agrego todos los estados
        _fsm.AddState(PlayerStatesEnum.Idle, new IdleStateMelee(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Chase, new ChaseStateMelee(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Attack, new AttackStateMelee(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.CDState, new CDStateMelee(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.IdleCDState, new IDLECDStateMelee(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.DetectionState, new DetectingMeleeState(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Hit, new HitMeleeState(_fsm, this));
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
        _fsm.ChangeState(PlayerStatesEnum.CDState);
    }

    //Esta funcion modifica la fuerza en la animacion con Animation Events
    public void BackwardsJump(int pushForce)
    {
        if (!justAttacked)
            attackForces = pushForce;
    }

    public void EndingDetectionAnimation()
    {
        _fsm.ChangeState(PlayerStatesEnum.Chase);
        Debug.Log("cambio a chase");
    }

    private void OnParticleCollision(GameObject other)
    {
        //Si me pega la bala de la shotgun Y NO ME PEGARON Y no estoy atacando Y no estoy en cd, triggereo hit state
        if (other.CompareTag("Bullet")/* && !enemyHitted*/&& !enemyHitted 
                && !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackOne-PESoldierTwo")
                && !anim.GetCurrentAnimatorStateInfo(0).IsName("CooldownAnimationState"))
        {
            Debug.Log("entre en cd anashe");
            _fsm.ChangeState(PlayerStatesEnum.Hit);
            if (this.gameObject.activeSelf)
               StartCoroutine(WaitForEnemyHitted());
        }
    }

    //Con esta funcion voy a cd state despues de que me pega la hueva, va al final de la animacion de hit
    public void ReturnToCdStateAfterGettingHit()
    {
        _fsm.ChangeState(PlayerStatesEnum.CDState);
    }

    IEnumerator WaitForEnemyHitted()
    {
        enemyHitted = true;
        yield return new WaitForSeconds(0.3f);
        enemyHitted = false;
    }

    //Funcion de animation event para activar y desactivar los colliders
    public void AttackingColliders()
    {
        if (!hitCollider.activeSelf)
            hitCollider.SetActive(true);
        else
            hitCollider.SetActive(false);
    }
}
