using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderVisualizer : MonoBehaviour
{
    Vector3 _circleCenter;
    float _circleRadius;

    public void GetGizmosParams(Vector3 circleCenter, float circleRadius)
    {
        _circleCenter = circleCenter;
        _circleRadius = circleRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_circleCenter, _circleRadius);
    }

}
