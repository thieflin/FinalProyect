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

    public float timerForGrabBack;
    public float timeToGrabBack;

    public float timerBetweenAbilities;
    public float timeBetweenAbilities;

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
    public bool grabLaunched;
    public bool grabBack;
    public float speedGrab;
    int randomOptionForGrab;
    Vector3 savePositionPlayer;
    Vector3 savePositionRightHand;
    Vector3 savePositionLeftHand;

    public GameObject bulletPrefab;
    public Transform[] spawnPositionsBullet;
    public bool startShooting;

    public int randomAttack;

    public AudioSource shootingAudio, walkingAudio, grabingAudio, hitAudio;
    bool hittingAudioPlayed;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        HP = maxHP;


        //Empieza con un ataque random
        //randomAttack = Random.Range(0, abilities.Length - 1);
        

    }

    // Update is called once per frame
    void Update()
    {

        if (resting)
        {
            Resting();
        }

        if (attacked)
        {
            rb.velocity = Vector3.zero;
            transform.Rotate(0f, 0f, 0f);
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
        else if (randomAttack == 3)
        {
            return;
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
        resting = false;

        randomAttack = Random.Range(0, 3);

        timerOnEachAttack = 0;

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
            walkingAudio.enabled = true;

            toPlayerVector = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z).normalized;
            transform.forward = toPlayerVector;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= distanceToAttack)
        {
            anim.SetBool("Walking", false);
            walkingAudio.enabled = false;
            hitAudio.enabled = true;
            rb.velocity = Vector3.zero;
            StartAttacking();
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > distanceToAttack && anim.GetBool("IsAttackingMelee"))
        {
            hitAudio.enabled = true;
        }

        if (!anim.GetBool("IsAttackingMelee"))
        {
            hitAudio.enabled = false;
        }


        if (timerOnEachAttack >= meleeDuration)
        {
            timerOnEachAttack = 0;
            RestAfterAbility();
        }
    }

    void Resting()
    {
        timerBetweenAbilities += Time.deltaTime;

        if (timerBetweenAbilities >= timeBetweenAbilities)
        {
            timerBetweenAbilities = 0f;
            StartCoroutine(FinishAttack());
        }
    }

    void StartShooting()
    {
        anim.SetBool("Shooting", true);
        anim.SetBool("Walking", false);
        walkingAudio.enabled = false;
        toPlayerVector = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z).normalized;
        transform.forward = toPlayerVector;
        transform.position = transform.position;
        rb.velocity = Vector3.zero;
        StartCoroutine(WaitBeforeShoot());
    }

    void StartGrabbing()
    {
        anim.SetBool("Walking", false);
        walkingAudio.enabled = false;
        timerOnEachAttack += Time.deltaTime;

        StartCoroutine(WaitForGrabbing());
    }

    void RestAfterAbility()
    {
        randomAttack = 99;

        attacking = false;
        shooting = false;

        grabing = false;
        resting = true;

        rb.velocity = Vector3.zero;

        anim.SetTrigger("GoToIdle");
        anim.SetBool("Walking", false);
        walkingAudio.enabled = false;
    }

    IEnumerator WaitForGrabbing()
    {
        if (!grabLaunched)
        {
            grabLaunched = true;

            toPlayerVector = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z).normalized;
            transform.forward = toPlayerVector;

            randomOptionForGrab = Random.Range(0, 2);

            yield return new WaitForSeconds(0.1f); //ESPERO UN POCO

            savePositionPlayer = player.transform.position + player.transform.forward * 5f;

            if (randomOptionForGrab == 0) //RIGHT HAND
            {
                anim.SetBool("HookRight", true);
            }
            else if (randomOptionForGrab == 1) //LEFT HAND
            {
                anim.SetBool("HookLeft", true);
            }


            savePositionLeftHand = handLeft.transform.localPosition;
            savePositionRightHand = handRight.transform.localPosition;

            AudioManager.PlaySound("RobotGrab");

            yield return new WaitForSeconds(0.5f);



            //LA MANO TIENE QUE SALIR DISPARADA EN ESTE MOMENTO

            rb.velocity = Vector3.zero;


            grabing = true;

        }

        //SI YA EMPECE A GRABBEAR
        if (grabing && !grabBack)
        {
            yield return new WaitForSeconds(0.5f); // ESPERO A QUE TERMINE LA ANIMACION PARA LANZAR LA MANO



            if (randomOptionForGrab == 0)
            {
                handRight.transform.position = Vector3.MoveTowards(handRight.transform.position, savePositionPlayer, speedGrab * Time.deltaTime);
            }

            else if (randomOptionForGrab == 1)
            {
                handLeft.transform.position = Vector3.MoveTowards(handLeft.transform.position, savePositionPlayer, speedGrab * Time.deltaTime);
            }

            yield return new WaitForSeconds(0.5f);

            grabing = false;
        }

        if (timerOnEachAttack >= grabDuration)
        {
            grabBack = true;

            timerForGrabBack += Time.deltaTime;

            if (grabBack)
            {

                if (randomOptionForGrab == 0)
                {
                    Debug.Log(savePositionRightHand);
                    handRight.transform.localPosition = Vector3.MoveTowards(handRight.transform.localPosition, savePositionRightHand, speedGrab * Time.deltaTime);
                }
                else if (randomOptionForGrab == 1)
                {
                    Debug.Log(savePositionLeftHand);
                    handLeft.transform.localPosition = Vector3.MoveTowards(handLeft.transform.localPosition, savePositionLeftHand, speedGrab * Time.deltaTime);
                }
            }
            //LE DOY ALGO DE TIEMPO PARA QUE VUELVA LA MANO
            if (timerForGrabBack >= timeToGrabBack)
            {
                grabBack = false;
                grabLaunched = false;
                timerForGrabBack = 0;
                timerOnEachAttack = 0;
                anim.SetBool("HookLeft", false);
                anim.SetBool("HookRight", false);
                RestAfterAbility();
            }

        }

    }
    IEnumerator WaitBeforeShoot()
    {
        yield return new WaitForSeconds(0.1f);
        startShooting = true;

        shootingAudio.enabled = true;

        timerOnEachAttack += Time.deltaTime;

        if (timerOnEachAttack >= shootingDuration)
        {
            anim.SetBool("Shooting", false);
            shootingAudio.enabled = false;
            startShooting = false;
            RestAfterAbility();
        }

    }



    IEnumerator Spawner()
    {

        foreach (var positions in spawnPositionsBullet)
        {
            yield return new WaitForSeconds(timeBetweenInternalShoots);

            Vector3 shootingDirection = player.transform.position /*+ player.transform.forward * 1f*/;

            shootingDirection = new Vector3(shootingDirection.x, shootingDirection.y + 4f, shootingDirection.z);

            var bullet = Instantiate(bulletPrefab, positions.transform.position, Quaternion.identity);
            var newBullet = bullet.GetComponent<BulletBoss>();


            var direction = (shootingDirection - newBullet.transform.position).normalized;

            newBullet.transform.forward = direction;


            newBullet.GetComponent<Rigidbody>().velocity = direction * newBullet.speed * Time.deltaTime;
            //newBullet.GetComponent<Rigidbody>().AddForce(direction * newBullet.speed * Time.deltaTime, ForceMode.Force);
        }
    }

    IEnumerator WaitToFinishAttackAnimation()
    {
        attacking = true;
        rb.velocity = Vector3.zero;
        walkingAudio.enabled = false;
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
        hittingAudioPlayed = false;
        anim.SetBool("IsAttackingMelee", false);
        timerAttacking = 0f;
        timerToCancel = 0f;
        return;
    }



}
