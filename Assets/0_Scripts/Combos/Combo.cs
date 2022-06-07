using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    //Maneja toda la logica del combo
    bool _isIdle = true;
    public Animator ani;
    int _nextCombo = 0;


    public List<GameObject> meleeHitboxes = new List<GameObject>();
    public List<ParticleSystem> pss = new List<ParticleSystem>();

    public ParticleSystem ps;

    public List<GameObject> rangedHitboxes = new List<GameObject>();

    public bool upgradedHitbox;
    public PlayerMovement _pm;

    public static int swordDmg;
    [SerializeField] private Rigidbody _rb;

    public bool isMovingBack;
    public bool isMoving;

    public ParticleSystem shotGunParticleSystem;

    [SerializeField] private float _regularSpeed;

    public GameObject particleDustSlide;
    public ParticleSystem particleDustStep;

    public bool canAttack;
    private void Awake()
    {
        _pm = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        swordDmg = 10;
        upgradedHitbox = false;
        canAttack = true;
        EventManager.Instance.Subscribe("OnGettingBiggerHitbox", UpgradedHitboxTrue);


    }
    void Update()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("AttackOne") || ani.GetCurrentAnimatorStateInfo(0).IsName("AttackTwo")
                                || ani.GetCurrentAnimatorStateInfo(0).IsName("AttackThree"))
        {
            Debug.Log("tukination");
            _pm.flagDash = false;
            Debug.Log(_pm.flagDash);
        }
        else
        {
            _pm.flagDash = true;

        }



        InputController();

        if (isMoving)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(transform.forward * Time.deltaTime * 100000, ForceMode.Force);
        }
        else if (isMovingBack)
        {
            _rb.velocity = Vector3.zero;

            _rb.AddForce(transform.forward * Time.deltaTime * -100000, ForceMode.Force);
        }


        if (Input.GetKeyDown(KeyCode.Y))
        {
            ani.SetTrigger("MS1");
        }
    }

    public void UpgradedHitboxTrue(params object[] parameters)
    {
        upgradedHitbox = true;
    }

    void InputController()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("AttackNA") && canAttack)
        {
            if (_isIdle == true)
            {
                _isIdle = false;
                _pm._movementSpeed = 0;
                _rb.velocity = Vector3.zero;
                //_rb.AddForce(transform.forward * Time.deltaTime * 1000, ForceMode.Force);
                //_rb.constraints = RigidbodyConstraints.FreezeAll;
                ani.SetTrigger("A1");
            }
            else
                _nextCombo = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetButtonDown("AttackRangedNA") && canAttack)
        {
            if (_isIdle == true)
            {
                _isIdle = false;
                _pm._movementSpeed = 0;
                _rb.velocity = Vector3.zero;
                //_rb.AddForce(transform.forward * Time.deltaTime * -1000, ForceMode.Force);
                //_rb.constraints = RigidbodyConstraints.FreezeAll;
                //INSTANCIO PARTICULAS
                ani.SetTrigger("A2");
            }
            else
                _nextCombo = 2;
        }

    }

    public void EventNextAnimation()
    {
        switch (_nextCombo)
        {
            case 0:
                ani.SetTrigger("Idle");
                _pm._movementSpeed = _regularSpeed;
                //_rb.constraints -= RigidbodyConstraints.FreezePosition;
                _isIdle = true;
                _pm.enabled = true;
                break;
            case 1:
                //_rb.constraints = RigidbodyConstraints.FreezeAll;
                _pm._movementSpeed = 0;
                //_rb.velocity = Vector3.zero;
                //_rb.AddForce(transform.forward * Time.deltaTime * 1000, ForceMode.Force);
                ani.SetTrigger("A1");

                _nextCombo = 0;
                break;
            case 2:
                _pm._movementSpeed = 0;
                //_rb.velocity = Vector3.zero;
                //_rb.AddForce(transform.forward * Time.deltaTime * -1000, ForceMode.Force);
                //_rb.constraints = RigidbodyConstraints.FreezeAll;
                ani.SetTrigger("A2");
                _nextCombo = 0;
                break;
        }
    }

    public void FinishCombo()
    {
        _nextCombo = 0;
        _pm._movementSpeed = _regularSpeed;
        ani.SetTrigger("Idle");
        _pm.enabled = true;
        _isIdle = true;
        isMoving = false;
        //_rb.constraints -= RigidbodyConstraints.FreezePosition;
    }


    public void MeleeColliderActivationCombo(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        if (meleeHitboxes[ColliderNumber].activeSelf)
        {
            isMoving = false;
            meleeHitboxes[ColliderNumber].SetActive(false);
        }
        else
        {
            isMoving = true;
            meleeHitboxes[ColliderNumber].SetActive(true);
            pss[ColliderNumber].Play();
        }
    }



    public void ActivateStepParticleStep()
    {
        particleDustStep.Play();
    }

    public void ActSlideSmokePart()
    {
        if (particleDustSlide.activeSelf)
            particleDustSlide.SetActive(false);
        else particleDustSlide.SetActive(true);
    }

    public void EnablePlayermovement()
    {
        _pm.enabled = true;
    }

    public void DisablePlayerMovement()
    {
        _pm.enabled = false;
    }

    public void StopDash()
    {
        _pm.canDash = false;
    }

    public void AllowDash()
    {
        _pm.canDash = true;
    }

    //Animation events 
    public void DisableAttack()
    {
        canAttack = false;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }

    public void ColliderActivationRangedCombo(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        //if (rangedHitboxes == null)
        //    return;

        shotGunParticleSystem.transform.position = gameObject.transform.position + (gameObject.transform.forward) * 1f + new Vector3(0, 2.5f, 0);
        shotGunParticleSystem.transform.rotation = gameObject.transform.rotation;

        shotGunParticleSystem.Play();

        //COMENTO POR AHORA PARA SACAR LOS COLLIDERS Y AGREGO EL SISTEMA DE PARTICULAS DIRECTAMENTE ACA

        if (rangedHitboxes[ColliderNumber].activeSelf)
        {
            isMovingBack = false;
            rangedHitboxes[ColliderNumber].SetActive(false);
        }
        else
        {
            isMovingBack = true;
            rangedHitboxes[ColliderNumber].SetActive(true);
        }
    }

    public void Sound()
    {
        AudioManager.PlaySound("slash");
    }
    
}