﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Control characterInputs;

    [SerializeField] private Rigidbody _rb;

    [Header("Movement values")]
    public float _movementSpeed;

    //RAYCASTS
    private Vector3 leftRayCast, rightRayCast, forwardRayCast, backwardRayCast;

    [Header("Rotation values")]
    [SerializeField] private float _smoothRotation = 0.1f;
    float turnSmoothVelocity;

    [Header("Dash values")]
    [SerializeField] private char lastMoveToDash;
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
    //[SerializeField] private List<float> enemyDistance;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    //Para testear unicamente
    public SphereCollider sph;

    private void Awake()
    {
        characterInputs = new Control(this);

        _rb = GetComponent<Rigidbody>();

        _targetLock = GetComponentInChildren<TargetLock>();

        animator = GetComponent<Animator>();
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

        //RAYCASTS PARA CAIDA
        leftRayCast = new Vector3(0, 0, 0.3f);
        leftRayCast += transform.position;

        rightRayCast = new Vector3(0, 0, -0.3f);
        rightRayCast += transform.position;

        forwardRayCast = new Vector3(0.3f, 0, 0);
        forwardRayCast += transform.position;

        backwardRayCast = new Vector3(-0.3f, 0, 0);
        backwardRayCast += transform.position;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(leftRayCast, Vector3.down, Color.green);
    }

    public void Move(Vector3 direction)
    {
        direction.Normalize();

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));

        var newInput = matrix.MultiplyPoint3x4(direction);

        //Walking Animation

        if ((direction.x != 0 || direction.z != 0) && !isTargeting)
        {
            animator.SetFloat("Speed", 1);
            animator.SetBool("IsTargeting", false);
        }
        else if ((direction.x == 0 || direction.z == 0) && !isTargeting)
        {
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsTargeting", false);
        }

        if (isTargeting)
        {
            Vector3 newVecX = direction.x * transform.right;
            Vector3 newVecZ = direction.z * transform.forward;



            animator.SetFloat("Speed", 1);
            animator.SetFloat("EjeX", newVecX.x);
            animator.SetFloat("EjeY", newVecZ.z);
            animator.SetBool("IsTargeting", true);
        }



        //NORMAL WALKING
        if (!isDashing && !isTargeting)
        {
            _rb.velocity = newInput * _movementSpeed * Time.fixedDeltaTime;
        }

        //Rotación

        if (!isTargeting)
        {
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

            lookAt.y = 0f;

            transform.forward = lookAt;

            //if (direction.x != 0 || direction.z != 0)
            //{
            //    if (enemyDistance.Count < 2)
            //    {
            //        enemyDistance.Add(Vector3.Distance(_lockedEnemy.transform.position, transform.position));
            //        StartCoroutine(waitForDistanceEnemy());
            //    }
            //    else if (enemyDistance.Count == 2)
            //    {
            //        enemyDistance.Clear();
            //    }
            //}

            if (!_targetLock.enemiesClose.Contains(_lockedEnemy))
            {
                isTargeting = false;
                //enemyDistance.Clear();
            }
        }

        //WHERE TO DASH
        //if (Vector3.Distance(_lockedEnemy.transform.position, transform.position) < Vector3.Distance(_lockedEnemy.transform.position, transform.position) && isTargeting)
        //{
        //    lastMoveToDash = 'S';
        //}
        //else if ((_lockedEnemy.transform.position - transform.position).normalized == direction && isTargeting)
        //{
        //    lastMoveToDash = 'W';
        //}
        ////else if (direction.x == 0 && direction.z < 0 && isTargeting)
        ////{
        ////    lastMoveToDash = 'S';
        ////}
        ////else if (direction.x == 0 && direction.z > 0 && isTargeting)
        ////{
        ////    lastMoveToDash = 'W';
        ////}
        //else if (!isTargeting)
        //{
        //    lastMoveToDash = 'N';
        //}
    }

    public void TargetEnemy(Enemy e, GameObject targetSign)
    {
        _lockedEnemy = e;

        //Lo pongo en la posicion del enemigo
        targetSign.transform.position = new Vector3(_lockedEnemy.transform.position.x, _lockedEnemy.transform.position.y - _lockedEnemy.transform.localScale.y
            + _lockedEnemy.offsetYTargetLock, _lockedEnemy.transform.position.z);
        //Lo hago hijo del enemigo para que lo siga
        targetSign.transform.parent = _lockedEnemy.transform;
        //Lo activo
        targetSign.SetActive(true);

        isTargeting = true;
    }

    public void Dash()
    {
        if (canDash)

            _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);

        StartCoroutine(waitDash());
        //switch (lastMoveToDash)
        //{
        //    case 'W':
        //        _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
        //        StartCoroutine(waitDash());
        //        break;
        //    case 'S':
        //        _rb.AddForce(transform.forward * -1 * _dashForce, ForceMode.Impulse);
        //        StartCoroutine(waitDash());
        //        break;
        //    case 'A':
        //        _rb.AddForce(transform.right * _dashForce, ForceMode.Impulse);
        //        StartCoroutine(waitDash());
        //        break;
        //    case 'D':
        //        _rb.AddForce(transform.right * -1 * _dashForce, ForceMode.Impulse);
        //        StartCoroutine(waitDash());
        //        break;
        //    case 'N':
        //        _rb.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
        //        StartCoroutine(waitDash());
        //        break;
        //    default:
        //        break;
        //}


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

    IEnumerator waitForDistanceEnemy()
    {
        yield return new WaitForSeconds(0.2f);
    }

    public bool isGrounded()
    {
        //EL RANGO VA A VARIAR CUANDO AGREGUE PJ MAIN
        if (Physics.Raycast(leftRayCast, Vector3.down, 0.3f) || Physics.Raycast(rightRayCast, Vector3.down, 0.3f) || Physics.Raycast(forwardRayCast, Vector3.down, 0.3f) ||
        Physics.Raycast(backwardRayCast, Vector3.down, 0.3f))
            return true;
        else
            return false;
    }
}