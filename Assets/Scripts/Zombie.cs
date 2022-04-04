using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        FieldOfView();

        if (playerIsInSight)
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
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < rangeAttack)
        {
            Attack();
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > rangeAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            animator.SetFloat("Speed", 1);
        }

    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

}
