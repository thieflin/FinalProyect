using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRender : MonoBehaviour
{
    public GameObject player;

    public LayerMask wallLayer;

    private void Update()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 15;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hitInfo, 50f, wallLayer))
        {
            var wallDetected = hitInfo.collider.gameObject.GetComponent<MeshRenderer>();
            wallDetected.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
    
    }
}
