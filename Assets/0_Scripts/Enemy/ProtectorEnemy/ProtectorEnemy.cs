using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorEnemy : Enemy
{
    public Enemy owner;
    public GameObject signLand;
    public GameObject signLandPrefab;
    public bool signCreated;
    public bool fallDownFaster;
    public float fallDownValue;

    private void Start()
    {
        if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hitInfo, Mathf.Infinity))
        {
            if (this.transform.position.y != owner.transform.position.y && !signCreated)
            {
                fallDownFaster = true;
                signLandPrefab = Instantiate(signLand, transform.position, signLand.transform.rotation);
                signLandPrefab.transform.position = hitInfo.point;
                signLandPrefab.transform.position = new Vector3(signLandPrefab.transform.position.x, signLandPrefab.transform.position.y + 0.2f, signLandPrefab.transform.position.z);
                signCreated = true;
            }
        }
    }

    private void Update()
    {
        FieldOfView();
        if (fallDownFaster)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f, -fallDownValue, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(signLandPrefab);
        transform.forward = owner.transform.forward;
        transform.parent = owner.transform;
        fallDownFaster = false;
    }
}
