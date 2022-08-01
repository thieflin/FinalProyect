using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boss : MonoBehaviour
{
    public enum Abilities
    {
        Melee,
        Shooting,
        Grab
    }

    Animator anim;
    Rigidbody rb;

    public Abilities[] abilities;
    public Abilities abilitySelected;

    [SerializeField] GameObject player;
    Vector3 toPlayerVector;

    [Header("Timers")]
    public float timerOnEachAttack;
    public float timerAttacking;
    public float timeToAttack;
    public float timerToCancel;
    public float timeFirstAttack;

    public float meleeDuration;
    public float shootingDuration;
    public float grabDuration;


    [Header("Boss Settings")]
    public float speed;
    public float maxHP;
    public float HP;
    public bool dead;
    public float distanceToAttack;
    public bool attacking;
    public bool attacked;
    public bool shooting;
    public bool grabing;
    public bool resting;
    public float timeBetweenShoots;
    public float timeBetweenInternalShoots;
    public float timerShooting;

    public GameObject handLeft, handRight;

    public GameObject bulletPrefab;
    public Transform[] spawnPositionsBullet;
    public bool startShooting;

    public int randomAttack;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        HP = maxHP;


        //Empieza con un ataque random
        //randomAttack = Random.Range(0, abilities.Length - 1);
        randomAttack = 0;


    }

    // Update is called once per frame
    void Update()
    {

        if (!attacked)
        {
            toPlayerVector = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z).normalized;
            transform.forward = toPlayerVector;
        }

        if (dead)
        {
            anim.SetTrigger("Dead");
            transform.forward = Vector3.zero;
        }

        if (randomAttack == 0)
        {
            SelectAttack(Abilities.Melee);
        }
        else if (randomAttack == 1)
        {
            SelectAttack(Abilities.Shooting);
        }
        else if (randomAttack == 2)
        {
            SelectAttack(Abilities.Grab);
        }


        if (startShooting)
        {
            SpawnBullets();
        }


        //if (Vector3.Distance(transform.position, player.transform.position) < distanceToAttack)
        //{
        //    attacking = true;
        //}

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
                attacked = true;
                StartCoroutine(WaitToFinishAttackAnimation());
            }
        }

        if (!attacking)
        {
            timerAttacking = 0f;
            timerToCancel = 0f;
            anim.SetBool("IsAttackingMelee", false);
        }
    }

    void SpawnBullets()
    {
        timerShooting += Time.deltaTime;

        if (timerShooting >= timeBetweenShoots)
        {
            timerShooting = 0f;

            StartCoroutine(Spawner());

        }
    }

    void SelectAttack(Abilities ability)
    {
        if (ability == Abilities.Melee)
        {
            abilitySelected = Abilities.Melee;
            attacking = true;
            shooting = false;
            startShooting = false;
            grabing = false;
            StartAttackingMelee();
        }

        if (ability == Abilities.Shooting)
        {
            abilitySelected = Abilities.Shooting;
            attacking = false;
            shooting = true;
            grabing = false;
            StartShooting();
        }

        if (ability == Abilities.Grab)
        {
            abilitySelected = Abilities.Grab;
            attacking = false;
            shooting = false;
            startShooting = false;
            grabing = true;
            StartGrabbing();
        }
    }

    IEnumerator FinishAttack()
    {
        randomAttack = Random.Range(0, 3);
        timerOnEachAttack = 0;
        Debug.Log("ENTRE ACA");
        startShooting = false;
        yield return new WaitForSeconds(0);
    }

    void StartAttackingMelee()
    {
        timerOnEachAttack += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) > distanceToAttack)
        {
            //Camino al player si esta muy lejos y tengo que atacar
            var direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * speed * Time.deltaTime;

            anim.SetBool("Walking", true);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= distanceToAttack)
        {
            anim.SetBool("Walking", false);
            rb.velocity = Vector3.zero;

            StartAttacking();
        }

        if (timerOnEachAttack >= meleeDuration)
        {
            timerOnEachAttack = 0;
            StartCoroutine(FinishAttack());
        }
    }

    void StartShooting()
    {
        anim.SetBool("Shooting", true);
        anim.SetBool("Walking", false);
        transform.position = transform.position;
        rb.velocity = Vector3.zero;
        StartCoroutine(WaitBeforeShoot());
    }

    void StartGrabbing()
    {

    }

    IEnumerator WaitBeforeShoot()
    {
        yield return new WaitForSeconds(0.1f);
        startShooting = true;

        timerOnEachAttack += Time.deltaTime;

        if (timerOnEachAttack >= shootingDuration)
        {
            anim.SetBool("Shooting", false);
           
            StartCoroutine(FinishAttack());
        }


    }

    IEnumerator Spawner()
    {

        foreach (var positions in spawnPositionsBullet)
        {
            yield return new WaitForSeconds(timeBetweenInternalShoots);

            Vector3 shootingDirection = player.transform.position + player.transform.forward * 2.5f;

            shootingDirection = new Vector3(shootingDirection.x, shootingDirection.y + 4f, shootingDirection.z);

            var bullet = Instantiate(bulletPrefab, positions.transform.position, Quaternion.identity);
            var newBullet = bullet.GetComponent<BulletBoss>();


            var direction = (shootingDirection - newBullet.transform.position).normalized;

            newBullet.transform.forward = direction;

            newBullet.GetComponent<Rigidbody>().AddForce(direction * newBullet.speed * Time.deltaTime, ForceMode.Force);
        }
    }

    IEnumerator WaitToFinishAttackAnimation()
    {
        attacking = true;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(2f);
        attacking = false;
        attacked = false;
    }

    void TakeDamage(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            dead = true;
        }
    }

    void StartAttacking()
    {
        anim.SetBool("IsAttackingMelee", true);

        attacked = true;

        transform.forward = transform.forward;

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
        attacked = false;
        anim.SetBool("IsAttackingMelee", false);
        timerAttacking = 0f;
        timerToCancel = 0f;
        return;
    }


}
