using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{

    public Collider[] targetInViewRadius;

    public float viewRadius;
    public float viewAngle;
    public LayerMask enemyLayer;
    public LayerMask obstacleMask;

    public List<Enemy> enemiesClose = new List<Enemy>();

    public PlayerMovement player;

    public GameObject targetSignPrefab;

    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        FieldOfView();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (enemiesClose.Count > 0 && !player.isTargeting)
                LookForClosestEnemy();
            else if (enemiesClose.Count > 0 && player.isTargeting)
            {
                player.isTargeting = false;
                targetSignPrefab.SetActive(false);
            }
            else
                Debug.Log("No hay enemigos cercanos");
        }

        if (!player.isTargeting)
        {
            targetSignPrefab.SetActive(false);
            targetSignPrefab.transform.parent = null;
        }
    }

    void LookForClosestEnemy()
    {
        Enemy closestEnemy = null;

        float closestDistance = (enemiesClose[0].transform.position - player.transform.position).magnitude;

        foreach (var enemy in enemiesClose)
        {
            float distance = (enemy.transform.position - player.transform.position).magnitude;

            if (distance <= closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        player.TargetEnemy(closestEnemy, targetSignPrefab);

    }

    private void FieldOfView()
    {
        targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, enemyLayer);

        foreach (var item in targetInViewRadius)
        {
            Vector3 dirToTarget = (item.transform.position - transform.position);
            if (Vector3.Angle(transform.forward, dirToTarget.normalized) < viewAngle / 2)
            {
                if (InSight(transform.position, item.transform.position))
                {
                    if (!enemiesClose.Contains(item.GetComponent<Enemy>()))
                        enemiesClose.Add(item.GetComponent<Enemy>());
                    Debug.DrawLine(transform.position, item.transform.position, Color.green);
                }
            }

            if((item.transform.position - transform.position).magnitude > viewRadius || !InSight(transform.position, item.transform.position))
            {
                enemiesClose.Remove(item.GetComponent<Enemy>());
            }
        }
    }

    public bool InSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        if (!Physics.Raycast(start, dir, dir.magnitude, obstacleMask)) return true;
        else return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Vector3 lineA = DirFromAngle(viewAngle / 2 + transform.eulerAngles.y);
        Vector3 lineB = DirFromAngle(-viewAngle / 2 + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * viewRadius);
    }

    Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
