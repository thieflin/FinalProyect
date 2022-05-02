using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorEnemy : Enemy
{
    [Header("Protector options")]
    public Enemy owner;
    public GameObject signLand;
    public GameObject signLandPrefab;
    public bool signCreated;
    public bool fallDownFaster;
    public float fallDownValue;
    public bool didExplote;
    public float radiusExplosion;
    public float explosionForce;
    public Vector3 landsAt;
    public LayerMask floorMask;
    public Rigidbody rbProtector;

    public Rigidbody playerRb;

    private void Start()
    {
        rbProtector = GetComponent<Rigidbody>();

        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        //StartCoroutine(AddForceToPlayer());

        if (Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hitInfo, Mathf.Infinity, floorMask))
        {
            if (this.transform.position.y != owner.transform.position.y && !signCreated)
            {
                fallDownFaster = true;
                transform.forward = owner.transform.forward;
                transform.parent = owner.transform;
                signLandPrefab = Instantiate(signLand, transform.position, signLand.transform.rotation);

                landsAt = hitInfo.point;

                signCreated = true;
            }
        }
    }

    private void FixedUpdate()
    {
        FieldOfView();
        if (fallDownFaster)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f, -fallDownValue * Time.fixedDeltaTime, 0f);
        }
        if (landsAt != null)
        {
            signLandPrefab.transform.position = new Vector3(transform.position.x, landsAt.y + 0.2f, transform.position.z);
            signLandPrefab.transform.forward = transform.forward;
        }

        float distanceWithPlayer = Vector3.Distance(playerRb.transform.position, transform.position);

        if (distanceWithPlayer <= 5 && !didExplote)
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(playerRb.transform.forward * -1 * explosionForce, ForceMode.Impulse);
            playerRb.GetComponent<CharStatus>().TakeDamage(20);
            didExplote = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        ////var playerRb = collision.gameObject.GetComponent<Rigidbody>();

        ////playerRb.AddForce(playerRb.transform.forward * -1 * explosionForce, ForceMode.Impulse);
        //if (collision.gameObject.CompareTag("Player") && !didExplote)
        //{
        //    playerRb.velocity = Vector3.zero;
        //    playerRb.AddForce(playerRb.transform.forward * -1 * explosionForce, ForceMode.VelocityChange);
        //    collision.gameObject.GetComponent<CharStatus>().TakeDamage(20);
        //    didExplote = true;
        //}

        signLandPrefab.SetActive(false);

        didExplote = true;

    }

}
