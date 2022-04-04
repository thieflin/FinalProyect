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
    public bool playerIsInSight;

    public GameObject player;

    [Header("Animator")]
    public Animator animator;

    public float rangeAttack;

    public void FieldOfView()
    {
        targetInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _playerMask);

        foreach (var item in targetInViewRadius)
        {
            Vector3 dirToTarget = (item.transform.position - transform.position);
            if (Vector3.Angle(transform.forward, dirToTarget.normalized) < _viewAngle / 2)
            {
                if (InSight(transform.position, item.transform.position))
                {
                    player = item.gameObject;
                    Debug.DrawLine(transform.position, item.transform.position, Color.red);
                    playerIsInSight = true;
                }
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
        Gizmos.color = Color.white;

        Vector3 lineA = DirFromAngle(_viewAngle / 2 + transform.eulerAngles.y);
        Vector3 lineB = DirFromAngle(-_viewAngle / 2 + transform.eulerAngles.y);
        Gizmos.DrawLine(transform.position, transform.position + lineA * _viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + lineB * _viewRadius);
    }

    Vector3 DirFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
