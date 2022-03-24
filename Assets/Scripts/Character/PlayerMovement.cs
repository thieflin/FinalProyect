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
    [SerializeField] private float _smoothRotation = 360;
    float turnSmoothVelocity;

    [Header("Dash values")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _timeBetweenDashes;
    [SerializeField] private bool canDash = true;
    private bool isDashing;

    [Header("Target Lock")]
    [SerializeField] private TargetLock _targetLock;
    [SerializeField] private bool _isTargeting;
    [SerializeField] private float _combatSpeed;
    [SerializeField] private Enemy _lockedEnemy;

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
    }


    public void Move(Vector3 direction)
    {
        direction.Normalize();

        if (!isDashing && !_isTargeting)
            _rb.velocity = direction * _movementSpeed * Time.fixedDeltaTime;

        //Rotación

        if (!_isTargeting)
        {
            if (direction.z >= 0.1f || direction.x >= 0.1f || direction.x <= -0.1 || direction.z <= -0.1)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _smoothRotation);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }

        if (_isTargeting)
        {
            transform.LookAt(_lockedEnemy.transform);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0);
            _rb.velocity = direction * _movementSpeed * Time.fixedDeltaTime;

            if (!_targetLock.enemiesClose.Contains(_lockedEnemy))
                _isTargeting = false;

        }
    }

    public void TargetEnemy(Enemy e)
    {
        _lockedEnemy = e;

        _isTargeting = true; ;
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
