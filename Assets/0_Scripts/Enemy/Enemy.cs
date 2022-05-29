using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Offset for target sign")]
    public float offsetYTargetLock;

    [Header("Movement enemy")]
    public float movementSpeed;


    [Header("Field of view")]
    private Collider[] targetInViewRadius;

    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private LayerMask _enemyWallMask;
    public bool playerIsInSight;

    [Header("Attack Trigger")]
    public Collider triggerAttack;

    public GameObject player;
    
    [Header("Animator")]
    public Animator animator;

    [Header("Wander")]
    public WanderVisualizer wanderVisualizer;
    public float circleRadius;
    public float circleDistance;
    private float _previousAngle;
    public float smallCircleRadius;
    public Vector3 velocity;
    public float maxForce;
    public float maxSpeed;
    public bool canRotate;

    public float rangeAttack;


    public bool waitForNextWanderPoint;

    private void Start()
    {
        wanderVisualizer = GetComponent<WanderVisualizer>();
        canRotate = true;
    }

    public void FieldOfView()
    {
        targetInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);

        foreach (var item in targetInViewRadius)
        {

            Vector3 dirToTarget = (item.transform.position - transform.position);

            //HAGO UN IF PARaA SABER QUE ESTEN EN LA MISMA POSICION O CERCA EN Y CON EL VALOR ABSOLUTO

            if (Vector3.Angle(transform.forward, dirToTarget.normalized) < _viewAngle / 2)
            {
                if (InSight(transform.position, item.transform.position))
                {
                    if (Mathf.Abs(transform.position.y - item.transform.position.y) < 2f && Mathf.Abs(transform.position.y - item.transform.position.y) > 0)
                    {
                        player = item.gameObject;
                        Debug.DrawLine(transform.position, item.transform.position, Color.red);
                        playerIsInSight = true;
                    }
                }
                else
                    playerIsInSight = false;
            }

            if ((item.transform.position - transform.position).magnitude > _viewRadius || !InSight(transform.position, item.transform.position))
            {
                playerIsInSight = false;
                player = null;
            }
        }
    }

    public bool InSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = end - start;
        if (!Physics.Raycast(start, dir, dir.magnitude, _obstacleLayer)) return true;
        else return false;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;

        //Vector3 lineA = DirFromAngle(_viewAngle / 2 + transform.eulerAngles.y);
        //Vector3 lineB = DirFromAngle(-_viewAngle / 2 + transform.eulerAngles.y);
        //Gizmos.DrawLine(transform.position, transform.position + lineA * _viewRadius);
        //Gizmos.DrawLine(transform.position, transform.position + lineB * _viewRadius);

    }

    void DrawWander(Vector3 circleCenter, Vector3 previousWanderPoint, Vector3 newPoint)
    {
        if (wanderVisualizer == null) return;

        wanderVisualizer.GetGizmosParams(circleCenter, circleRadius, previousWanderPoint, smallCircleRadius, newPoint);
    }

    Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public Vector3 Wander()
    {

        
        Vector3 circleCenter = transform.position + transform.forward * circleDistance;
        Vector3 previousWanderAngle = DirFromAngle(_previousAngle + transform.eulerAngles.y);
        Vector3 previousWanderPoint = circleCenter + previousWanderAngle * circleRadius;

        Vector3 randomVector = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        randomVector = randomVector.normalized * smallCircleRadius;
        Vector3 newPoint = previousWanderPoint + randomVector;

        Vector3 linePoint = newPoint - circleCenter;
        Vector3 newWanderPoint = circleCenter + linePoint.normalized * circleRadius;

        Vector3 oldDir = circleCenter + previousWanderPoint;
        Vector3 newDir = circleCenter + newWanderPoint;

        float angle = Vector3.SignedAngle(oldDir, newDir, circleCenter + Vector3.up);
        _previousAngle += angle;
        if (_previousAngle > 360) _previousAngle -= 360;
        if (_previousAngle < 0) _previousAngle += 360;


        DrawWander(circleCenter, previousWanderPoint, newPoint);
        return Seek(newWanderPoint);

    }

    IEnumerator stopPls()
    {
        yield return new WaitForSeconds(1f);
    }


    Vector3 Seek(Vector3 target)
    {
        Vector3 desired = (target - transform.position).normalized * maxSpeed;
        Vector3 steering = Vector3.ClampMagnitude(desired - velocity, maxForce);
        return steering;
    }

    public void AddForce(Vector3 force)
    {
        velocity = Vector3.ClampMagnitude(velocity + force, maxSpeed);
    }

    public bool isFacingWall()
    {
        if (Physics.Raycast(transform.position, transform.forward, 1.8f, _enemyWallMask))
        {
            StartCoroutine(WaitToRotate());
            return true;
        }
        else return false;
    }

    public IEnumerator WaitToRotate()
    {
        yield return new WaitForSeconds(1f);
        canRotate = true;
    }
}
