using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Control characterInputs;

    [Header("Movement values")]
    [SerializeField] private float movementSpeed;

    [Header("Rotation values")]
    [SerializeField] private float smoothRotation = 0.25f;
    float turnSmoothVelocity;

    private void Awake()
    {
        characterInputs = new Control(this);
    }

    private void FixedUpdate()
    {
        characterInputs.OnUpdate();
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * movementSpeed * Time.deltaTime;



        //Rotación
        if (direction.z >= 0.1f || direction.x >= 0.1f || direction.x <= -0.1 || direction.z <= -0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, smoothRotation);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    public void Dash()
    {

    }
}
