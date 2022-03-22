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

    private void Awake()
    {
        characterInputs = new Control(this);

        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        characterInputs.OnUpdate();

    }

    private void Update()
    { 
       
    }


    public void Move(Vector3 direction)
    {
        direction.Normalize();

        _rb.velocity = direction * _movementSpeed * Time.fixedDeltaTime;



        //Rotación
        if (direction.z >= 0.1f || direction.x >= 0.1f || direction.x <= -0.1 || direction.z <= -0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, _smoothRotation);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }

    public void Dash()
    {
        if (canDash)
        {
            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
            Debug.Log("Is dashing");
            StartCoroutine(waitDash());
        }

    }

    IEnumerator waitDash()
    {
        canDash = false;

        yield return new WaitForSeconds(_dashTime);
        _rb.velocity = Vector3.zero;
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
