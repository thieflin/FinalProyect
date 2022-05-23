using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform actualTransform;

    public float speed;


    void Start()
    {
        transform.parent = null;
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localPosition = Camera.main.transform.forward * -1 * 30;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position += (actualTransform.position - transform.position) * speed * Time.fixedDeltaTime;
    }
}
