using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [Header("Range Enemy options")]
    public GameObject bulletPrefab;
    [SerializeField] private float _timeBtwShots;
    [SerializeField] private float startTimeBtwShots;

    [Header("Move range enemy")]
    public float stoppingDistance;
    public float goBackDistance;
    public int multiplierRunningAway;
    private Ray checkWallsRay;
    [SerializeField] LayerMask _wallMask;

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

       
    }

    private void Attack()
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

    private void Move()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

        if (Vector3.Distance(transform.position, player.transform.position) > stoppingDistance && playerIsInSight)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, movementSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < stoppingDistance && Vector3.Distance(transform.position, player.transform.position) > goBackDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < goBackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, -movementSpeed * multiplierRunningAway * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * -8, Color.red);
    }
}