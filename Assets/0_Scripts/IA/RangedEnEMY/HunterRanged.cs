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
    public float idleWpCd; //CD De cada waypoint

    [Header("Idle")]
    public float rotationSpeedOnIdle; //Que tan rapido rota en el idle
    public float waitForNextAttack;

    [Header("Target & Others")]
    public PlayerMovement target; //El player

    //Estas 3 distancias sirven para que cuando lo detecte, en base a la distancia en la que este, haga una de las acciones
    public float detectDistance; //Distancia para que lo detecte y lo persiga
    public float stepbackDistance; //Distancia para tomar el stepback
    public float chaseDistance; //Esta distancia es post deteccion

    public float attackDistance; //Attack distance es para poder hacer que se vaya hacia atras
    public float loseTargetDistance; //Distancia a lo cual lo pierde y vuelve a lo suyo
    public int attackForces; //Cuanta fuerza tiene el movimiento con el que se desplaza
    public int knockbackForce; //Fuerza del knock back
    public int layerHit; //Layer de colision
    public bool isTargetting; //Variable para que cuadno carga el ataque me mire
    public List<GameObject> hitColliders = new List<GameObject>(); //Colliders de las garras
    public int damageDone;//Da;o que hace al player
    public bool justAttacked; //Booleano para saber si le pego al player
    public float attackCd;
    public bool enemyHitted;//Cuando recibe dmg que entre en cd
    public float distanceToPlayer;//Cuando recibe dmg que entre en cd


    public RangedBullet bullet;
    public GameObject spawnBulletPos;

    [Header("Cosmetics")]
    public GameObject particleSystemXD;

    public Rigidbody rb;
    public Animator anim;

    public DetectionBox detectionBox;

    private StateMachine _fsm;


    void Start()
    {
        _fsm = new StateMachine();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        target = FindObjectOfType<PlayerMovement>();

        distanceToPlayer = 10;

        _fsm.AddState(PlayerStatesEnum.Patrol, new WaypointStateRanged(_fsm, this)); //Agrego todos los estados
        _fsm.AddState(PlayerStatesEnum.Idle, new IdleStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Chase, new ChaseStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Attack, new AttackStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.StepbackState, new StepbackStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.CDState, new CDStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.DetectionState, new DetectedStateRanged(_fsm, this));
        _fsm.AddState(PlayerStatesEnum.Hit, new HitStateRanged(_fsm, this));
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
            attackForces = pushForce;
    }

    //Animation event al final de la animacion de stepback, procede a atacar cuadno temrina
    public void StartAttack()
    {
        _fsm.ChangeState(PlayerStatesEnum.Attack);
    }

    //Animation event final del ataque para ir al cd
    public void EndAttack()
    {
        _fsm.ChangeState(PlayerStatesEnum.CDState);
    }

    public void EndOfDetection()
    {
        _fsm.ChangeState(PlayerStatesEnum.StepbackState);
        Debug.Log("cambio a chase");
    }


    private void OnParticleCollision(GameObject other)
    {
        //Si me pega la bala de la shotgun Y NO ME PEGARON Y no estoy atacando Y no estoy en cd, triggereo hit state
        if (other.CompareTag("Bullet")/* && !enemyHitted*/&& !enemyHitted
                && !anim.GetCurrentAnimatorStateInfo(0).IsName("Stepback_PESoldierOne")
                && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting_PESoldierOne")
                && !anim.GetCurrentAnimatorStateInfo(0).IsName("CooldownSoldierRanged"))
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

    public void IsntantiateBullet()
    {
        var instantiateBullet = Instantiate(bullet, spawnBulletPos.transform.position, transform.rotation);
    }

}
