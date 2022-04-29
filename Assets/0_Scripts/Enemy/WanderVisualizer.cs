using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderVisualizer : MonoBehaviour
{
    Vector3 _circleCenter;
    float _circleRadius;
    Vector3 _previousWanderPoint;
    float _smallCircleRadius;
    Vector3 _newPoint;

    public void GetGizmosParams(Vector3 circleCenter, float circleRadius,Vector3 previousWanderPoint, float smallCircleRadius, Vector3 newPoint)
    {
        _circleCenter = circleCenter;
        _circleRadius = circleRadius;
        _previousWanderPoint = previousWanderPoint;
        _smallCircleRadius = smallCircleRadius;
        _newPoint = newPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_circleCenter, _circleRadius);
        Gizmos.DrawWireSphere(_previousWanderPoint, _smallCircleRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(_circleCenter, _previousWanderPoint);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_previousWanderPoint, _newPoint);

        Gizmos.color = Color.red;
        Vector3 linePoint = _newPoint - _circleCenter;
        Vector3 newWanderPoint = _circleCenter + linePoint.normalized * _circleRadius;
        Gizmos.DrawLine(_circleCenter, newWanderPoint);
        Gizmos.DrawSphere(newWanderPoint, 0.25f);
    }

}
