using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Control characterInputs;

    [SerializeField] private Rigidbody _rb;

    [Header("Movement values")]
    [SerializeField] private float _movementSpeed;

    [Header("Rotation values")]
    [SerializeField] private float _smoothRotation = 0.1f;
    float turnSmoothVelocity;

    [Header("Dash values")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _timeBetweenDashes;
    [SerializeField] private bool canDash = true;
    private bool isDashing;

    [Header("Target Lock")]
    [SerializeField] private TargetLock _targetLock;
    [SerializeField] public bool isTargeting;
    [SerializeField] private float _combatSpeed;
    [SerializeField] private Enemy _lockedEnemy;


    //Para testear unicamente
    public SphereCollider sph;

    private void Awake()
    {
        characterInputs = new Control(this);

        _rb = GetComponent<Rigidbody>();

        _targetLock = GetComponentInChildren<TargetLock>();
    }

    private void FixedUpdate()
    {
        characterInputs.OnUpdate();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Dash();

        //Para testear unicamente
        if (Input.GetKeyDown(KeyCode.I))
            if (sph.enabled == true)
                sph.enabled = false;
            else sph.enabled = true;

    }


    public void Look(Vector3 direction)
    {
        //var relative = (transform.position - direction) - transform.position;
        //var rot = Quaternion.LookRotation(relative, Vector3.up);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 360 * Time.fixedDeltaTime);
    }

    public void Move(Vector3 direction)
    {
        direction.Normalize();

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));

        var newInput = matrix.MultiplyPoint3x4(direction);

        //NORMAL WALKING
        if (!isDashing && !isTargeting)
        {
            _rb.velocity = newInput * _movementSpeed * Time.fixedDeltaTime;
        }

        //Rotación

        if (!isTargeting)
        {

            var matrixRotation = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));

            var newInputRotation = matrix.MultiplyPoint3x4(direction);

            if (newInputRotation.z >= 0.1f || newInputRotation.x >= 0.1f || newInputRotation.x <= -0.1 || newInputRotation.z <= -0.1)
            {
                float targetAngle = Mathf.Atan2(newInputRotation.x, newInputRotation.z) * Mathf.Rad2Deg;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _smoothRotation);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

        //TARGETING WALKING
        if (isTargeting)
        {
            if (!isDashing)
                _rb.velocity = newInput * _movementSpeed * Time.fixedDeltaTime;

            Vector3 lookAt = (_lockedEnemy.transform.position - transform.position).normalized;

            transform.forward = lookAt;

            if (!_targetLock.enemiesClose.Contains(_lockedEnemy))
                isTargeting = false;

        }
    }

    public void TargetEnemy(Enemy e)
    {
        _lockedEnemy = e;

        isTargeting = true; ;
    }

    public void Dash()
    {
        if (canDash)
        {
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
            Debug.Log("Is dashing");
            StartCoroutine(waitDash());
        }

    }

    IEnumerator waitDash()
    {
        canDash = false;
        isDashing = true;
        yield return new WaitForSeconds(_dashTime);
        _rb.velocity = Vector3.zero;
        isDashing = false;
        yield return new WaitForSeconds(_timeBetweenDashes);
        canDash = true;
    }

    private void OnCollisionStay(Collision collision)
    {

        _rb.AddTorque(Vector3.zero);

    }

    private void OnCollisionExit(Collision collision)
    {
        _rb.AddTorque(Vector3.zero);

    }
}
