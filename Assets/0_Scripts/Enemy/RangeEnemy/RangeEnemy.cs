using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Range Enemy options")]
    public GameObject bulletPrefab;
    [SerializeField] private float _timeBtwShots;
    [SerializeField] private float startTimeBtwShots;
    public Rigidbody _rb;

    [Header("Move range enemy")]
    public float stoppingDistance;
    public float goBackDistance;
    public int multiplierRunningAway;
    public float backDashForce;
    private Ray checkWallsRay;
    public float timeForDash;
    [SerializeField] LayerMask _wallMask;
    bool onCooldown;
    bool canAttack;
    public float secondsOnStay;

    //[Header("Ranged Protector")]
    //public ProtectorEnemy protectorPrefab;
    //public float percentHPToInvoke;
    //public bool protectorInvoked;
    //public float offSetYProtector;
    //public float offSetForwardProtector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        FieldOfView();

        if (playerIsInSight)
        {
            Vector3 lookAt = (player.transform.position - transform.position).normalized;
            lookAt.y = 0f;
            transform.forward = lookAt;

            Attack();

            //PARA QUE SE VAYA PARA ATRAS SOLO CUANDO NO HAYA UNA PARED
            if (!Physics.Raycast(transform.position, transform.forward * -1, 1f, _wallMask))
            {
                Move();
            }
        }

        //if ((this.gameObject.GetComponent<EnemyData>().GetHP() / this.gameObject.GetComponent<EnemyData>().GetMaxHP()) < percentHPToInvoke)
        //{
        //    if (!protectorInvoked) { 
        //        InvocarProtector();
        //    }
        //}

        if (!playerIsInSight && !isFacingWall())
        {
            AddForce(Wander());
            transform.position += velocity * Time.deltaTime;
            transform.forward = velocity.normalized;
        }

        if (isFacingWall() && !playerIsInSight && canRotate)
        {
            transform.Rotate(0, 180f, 0);
            velocity *= -1;
            canRotate = false;
        }
    }

    private void Attack()
    {
        if (CanAttack())
        {
            if (_timeBtwShots <= 0)
            {
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                _timeBtwShots = startTimeBtwShots;
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }
        }
    }

    bool CanAttack()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        if (Vector3.Distance(transform.position, player.transform.position) < rangeAttack && playerIsInSight)
            return true;
        else
            return false;
    }

    private void Move()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        if (Vector3.Distance(transform.position, player.transform.position) < goBackDistance && playerIsInSight)
        {
            StartCoroutine(DashBack());
        }

        if (Vector3.Distance(transform.position, player.transform.position) > stoppingDistance && playerIsInSight && !onCooldown) //ON SLEEPING
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, movementSpeed * Time.deltaTime);
        }

    }

    IEnumerator DashBack()
    {
        if (!onCooldown)
        {
            _rb.AddForce(backDashForce * transform.forward * -1 * Time.deltaTime, ForceMode.Force);
            yield return new WaitForSeconds(timeForDash);
            _rb.velocity = Vector3.zero;
            onCooldown = true;
            yield return new WaitForSeconds(secondsOnStay);
            onCooldown = false;
        }
    }

    //private void InvocarProtector()
    //{
    //    Debug.Log("InvocarProtector");

    //    Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + offSetYProtector, transform.position.z) + transform.forward * offSetForwardProtector;

    //    var createdProtector = Instantiate(protectorPrefab, spawnPosition, Quaternion.identity);
    //    createdProtector.owner = this;
    //    protectorInvoked = true;
    //}

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * -8, Color.red);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitSecondTakeOffForce());
        }
    }

    IEnumerator WaitSecondTakeOffForce()
    {
        yield return new WaitForSeconds(0.05f);
        _rb.velocity = Vector3.zero;
    }
}
