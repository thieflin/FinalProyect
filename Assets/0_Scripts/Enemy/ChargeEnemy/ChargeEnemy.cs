using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    [Header("Charge enemy options")]
    public float chargeForce;
    public float closeToAttack;
    public float timeToRegenerate;
    public float timeBtwAttacks;
    public bool isResting;
    public float chargeAttackDuration;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        FieldOfView();

        if (playerIsInSight)
        {
            Vector3 lookAt = (player.transform.position - transform.position).normalized;
            lookAt.y = 0f;
            transform.forward = lookAt;
        }

        if (!isResting && playerIsInSight)
        {
            CloseToPlayer();
            timeBtwAttacks -= Time.deltaTime;
        }

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

    void CloseToPlayer()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        if (Vector3.Distance(transform.position, player.transform.position) > closeToAttack && playerIsInSight)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, movementSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= closeToAttack && playerIsInSight)
        {
            if (player != null)
                Attack();
        }
    }

    void Attack()
    {
        if (timeBtwAttacks <= 0)
        {
            isResting = true;
            timeBtwAttacks = timeToRegenerate;
            Debug.Log("Cargo ataque y lanzo ataque");
            StartCoroutine(ChargeAttack());

        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }

    IEnumerator ChargeAttack()
    {
        rb.AddForce(transform.forward * -5, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * chargeForce, ForceMode.Impulse);
        yield return new WaitForSeconds(chargeAttackDuration);
        rb.velocity = Vector3.zero;
        isResting = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isResting)
        {
            collision.gameObject.GetComponent<CharStatus>().TakeDamage(20);
            Debug.Log("PEGUE AL PLAYER CON CHARGER");
        }
    }
}
