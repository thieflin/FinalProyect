using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    Animator anim;
    Rigidbody rb;


    [SerializeField] GameObject player;
    Vector3 toPlayerVector;

    [Header("Timers")]
    public float timerAttacking;
    public float timeToAttack;
    public float timerToCancel;
    public float timeFirstAttack;

    [Header("Boss Settings")]
    public float distanceToAttack;
    public bool attacking;
    public bool shooting;
    public float timeBetweenShoots;
    public float timeBetweenInternalShoots;
    public float timerShooting;

    public GameObject bulletPrefab;
    public Transform[] spawnPositionsBullet;
    public bool startShooting;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        toPlayerVector = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z).normalized;
        transform.forward = toPlayerVector;

        if (startShooting)
        {
            SpawnBullets();
        }

        if (Vector3.Distance(transform.position, player.transform.position) < distanceToAttack)
        {
            attacking = true;
            
        }

        if (attacking)
        {
            timerAttacking += Time.deltaTime;
            timerToCancel += Time.deltaTime;

            StartAttacking();

            if (timerAttacking <= timeFirstAttack && Vector3.Distance(transform.position, player.transform.position) > distanceToAttack)
            {
                StopAttacking();
                
            }
            else
            {
                StartCoroutine(WaitToFinishAttackAnimation());
            }
        }

        //if (/*Vector3.Distance(transform.position, player.transform.position) >= distanceToAttack && timerToCancel <= timeFirstAttack*/!attacking)
        //{
        //    StopAttacking();
        //}

    }

    void SpawnBullets()
    {
        timerShooting += Time.deltaTime;

        if(timerShooting >= timeBetweenShoots)
        {
            timerShooting = 0f;

            StartCoroutine(Spawner());

        }
    }

    IEnumerator Spawner()
    {
        Vector3 shootingDirection = player.transform.position + player.transform.forward * 1;

        
        foreach (var positions in spawnPositionsBullet)
        {
            yield return new WaitForSeconds(timeBetweenInternalShoots);
            var bullet = Instantiate(bulletPrefab, positions.transform.position, Quaternion.identity);
            var newBullet = bullet.GetComponent<BulletBoss>();

            var direction = (shootingDirection - newBullet.transform.position).normalized;

            newBullet.GetComponent<Rigidbody>().AddForce(direction * newBullet.speed * Time.fixedDeltaTime, ForceMode.Force);
        }
    }

    IEnumerator WaitToFinishAttackAnimation()
    {
        attacking = true;
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    void StartAttacking()
    {
        anim.SetBool("IsAttackingMelee", true);

        

        if (timerAttacking >= timeToAttack)
        {
            timerAttacking = 0f;
            timerToCancel = 0f;
            attacking = false;
            StopAttacking();
        }
    }


    void StopAttacking()
    {
        anim.SetBool("IsAttackingMelee", false);
        timerAttacking = 0f;
        timerToCancel = 0f;
        return;
    }


}
