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

    private void Start()
    {
        rbProtector = GetComponent<Rigidbody>();

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !didExplote)
        {
            Explosion();
            fallDownFaster = false;
        }
        if (collision.gameObject.CompareTag("Player") && !didExplote)
        {
            Debug.Log("GOLPEO AL PLAYER CON EL PROTECTOR");

            Explosion();
        }
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusExplosion);


        foreach (var player in colliders)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();

            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            if (rb != null && rb.gameObject.CompareTag("Player"))
            {
                //rb.AddExplosionForce(explosionForce, signLandPrefab.transform.position, radiusExplosion);
                rb.AddForce(directionToPlayer * explosionForce * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }

        signLandPrefab.SetActive(false);

        didExplote = true;
    }
}
