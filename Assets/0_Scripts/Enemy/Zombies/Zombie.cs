using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public bool canFollow;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        canFollow = true;
    }


    private void Update()
    {
        FieldOfView();

        if (playerIsInSight && Mathf.Abs(transform.position.y - player.transform.position.y) < 2f && Mathf.Abs(transform.position.y - player.transform.position.y) > 0)
        {
            Vector3 lookAt = (player.transform.position - transform.position).normalized;
            lookAt.y = 0f;
            transform.forward = lookAt;
            FollowPlayer();

        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (!canFollow)
        {

        }

    }
    float timeLeft;
    private void FollowPlayer()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < rangeAttack)
        {
            Attack();
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > rangeAttack)
        {
            if (canFollow)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
                animator.SetFloat("Speed", 1);
            }
            else
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    timeLeft = 3f;
                    canFollow = true;
                }
            }
        }

    }

    private void Attack()
    {
        animator.SetTrigger("Attack"); 
        canFollow = false;
        timeLeft = 3f;


    }
}
