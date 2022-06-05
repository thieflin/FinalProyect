using System.Collections;
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
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _timeBetweenDashes;
    public bool canDash = true;
    private bool isDashing;
    bool dashBarIsEmpty;
    public GameObject dashPart;
    private Vector3 whereToDash;
    [SerializeField] DashBar dashBar;
    public bool flagDash; //Esta variable me sirve para desahabilitar el dash pero no con el cd, simplemente desactivarlo
    //para que no se pueda usarlo

    [Header("Target Lock")]
    [SerializeField] public bool isTargeting;
    [SerializeField] private float _combatSpeed;
    [SerializeField] private Enemy _lockedEnemy;
    //[SerializeField] private List<float> enemyDistance;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    [Header("Slope values")]
    public float maxSlopeAngle;
    private RaycastHit _slopeHit;

    //Para testear unicamente
    public SphereCollider sph;

    private void Awake()
    {
        characterInputs = new Control(this);

        _rb = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();


        dashBar.dashSlider.maxValue = _timeBetweenDashes;
        dashBar.dashSlider.value = _timeBetweenDashes;
    }

    private void FixedUpdate()
    {
        characterInputs.OnUpdate();

    }

    private void Update()
    {

        ////Esto es porque por algun motivo me las desfreezea cuando combea medio xd el tema
        //_rb.constraints = RigidbodyConstraints.FreezeRotation;
        if (flagDash)
        {
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash")) && !isTargeting && isGrounded() && flagDash)
                Dash(Vector3.zero);
            else if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Dash")) && isTargeting && isGrounded() && flagDash)
                Dash(Vector3.zero);
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if (canDash)
            {
                var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));

                var newInput = matrix.MultiplyPoint3x4(new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y")));

                newInput.Normalize();

                //Debug.Log(Input.GetAxisRaw("Mouse X") + " MOUSE X");

                //Debug.Log(Input.GetAxisRaw("Mouse Y") + " MOUSE Y");

                Dash(newInput);
            }
        }

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

        if (dashBarIsEmpty)
        {
            if (dashBar.dashSlider.value < _timeBetweenDashes)
                dashBar.dashSlider.value += Time.deltaTime;
            else
                dashBarIsEmpty = false;
        }

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(leftRayCast, Vector3.down * 0.2f, Color.green);
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

        //EN ESCALERAS
        if (OnSlope())
        {
            if (newInput.x != 0 && newInput.z != 0 && !isDashing)
                _rb.velocity = (GetSlopeMoveDirection(newInput) * _movementSpeed * Time.fixedDeltaTime);

        }

        //QUITAR GRAVITY EN SLOPE
        _rb.useGravity = !OnSlope();

        //Rotación

        if (!isTargeting && !isDashing)
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
            {
                _rb.velocity = newInput * _movementSpeed * Time.fixedDeltaTime;
                Vector3 lookAt = (_lockedEnemy.transform.position - transform.position).normalized;
                lookAt.y = 0f;
                transform.forward = lookAt;
            }
            if (!TargetLock.enemiesClose.Contains(_lockedEnemy))
            {
                isTargeting = false;
            }
        }

        if (isDashing && !isGrounded())
        {
            _rb.AddForce(Vector3.down * 300, ForceMode.Acceleration);

        }

        if (isDashing && !OnSlope())
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        }

        //Hacia donde va a hacer el dash, segun movimiento
        whereToDash = new Vector3(newInput.x, 0, newInput.z);

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

    //Para cuando dashea y esta targeteando
    public void Dash(Vector3 whereToDashes)
    {
        if (whereToDashes != Vector3.zero)
        {
            whereToDash = whereToDashes;
        }

        if (canDash)
        {
            //_rb.velocity = Vector3.zero;

            if (whereToDash == Vector3.zero)
                whereToDash = transform.forward;

            if (OnSlope())
            {
                whereToDash = GetSlopeMoveDirection(whereToDash);
            }


            if (whereToDash != Vector3.zero)
            {
                _rb.AddForce(whereToDash * _dashForce * Time.fixedDeltaTime, ForceMode.Impulse);
                transform.forward = whereToDash;
                StartCoroutine(waitDash());
            }

            animator.SetTrigger("Rol");
        }


    }

    IEnumerator waitDash()
    {
        _rb.velocity = Vector3.zero;
        canDash = false; //Is dashing
        isDashing = true; //Esta dashing
        dashPart.SetActive(true);//Particulas
        dashBar.dashSlider.value -= _timeBetweenDashes; //Slider
        dashBarIsEmpty = true;//Slider
        var saveYRotation = transform.eulerAngles.y;
        yield return new WaitForSeconds(_dashTime);
        yield return new WaitForSeconds(0.2f);
        transform.eulerAngles = new Vector3(0f, saveYRotation, 0f);
        isDashing = false;
        yield return new WaitForSeconds(_timeBetweenDashes - 0.2f);
        canDash = true;
        dashPart.SetActive(false);

    }


    public bool isGrounded()
    {
        //EL RANGO VA A VARIAR CUANDO AGREGUE PJ MAIN
        if (Physics.Raycast(leftRayCast, Vector3.down, 0.2f) || Physics.Raycast(rightRayCast, Vector3.down, 0.2f) || Physics.Raycast(forwardRayCast, Vector3.down, 0.2f) ||
        Physics.Raycast(backwardRayCast, Vector3.down, 0.2f))
            return true;
        else
            return false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, transform.localScale.y * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized;
    }

    public void DeactivateDahsOnAttack()
    {
        flagDash = false;
    }

    public void ActivateDashOnAttack()
    {
        flagDash = true;
    }

}