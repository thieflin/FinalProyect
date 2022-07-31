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
