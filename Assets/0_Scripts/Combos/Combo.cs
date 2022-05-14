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
    public List<GameObject> meleeHitboxesUpgraded = new List<GameObject>();


    public List<GameObject> rangedHitboxes = new List<GameObject>();

    public bool upgradedHitbox;
    private PlayerMovement _pm;
    public static int swordDmg;
    [SerializeField] private Rigidbody _rb;

    public bool isMovingBack;
    public bool isMoving;

    public ParticleSystem shotGunParticleSystem;

    private void Start()
    {
        swordDmg = 10;
        upgradedHitbox = false;
        EventManager.Instance.Subscribe("OnGettingBiggerHitbox", UpgradedHitboxTrue);
        _pm = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        InputController();

        if (isMoving)
        {
            _rb.velocity = Vector3.zero;
            _rb.AddForce(transform.forward * Time.deltaTime * 1000, ForceMode.Force);
        }
        else if (isMovingBack)
        {
            _rb.velocity = Vector3.zero;

            _rb.AddForce(transform.forward * Time.deltaTime * -1000, ForceMode.Force);
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
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("AttackNA"))
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
        else if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetButtonDown("AttackRangedNA"))
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
                _pm._movementSpeed = 450;
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
        _pm._movementSpeed = 450;
        ani.SetTrigger("Idle");
        _pm.enabled = true;
        _isIdle = true;
        isMoving = false;
        //_rb.constraints -= RigidbodyConstraints.FreezePosition;
    }

    public void HitBoxMelee1Activate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[0].SetActive(true);
        else meleeHitboxesUpgraded[0].SetActive(true);
        isMoving = true;
    }


    public void HitBoxMelee2Activate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[1].SetActive(true);
        else meleeHitboxesUpgraded[1].SetActive(true);
        isMoving = true;

    }

    public void HitBoxMelee3Activate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[2].SetActive(true);
        else meleeHitboxesUpgraded[2].SetActive(true);
        isMoving = true;

    }

    public void HitBoxMelee1Deactivate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[0].SetActive(false);
        else meleeHitboxesUpgraded[0].SetActive(false);
        isMoving = false;

    }
    public void HitBoxMelee2Deactivate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[1].SetActive(false);
        else meleeHitboxesUpgraded[1].SetActive(false);
        isMoving = false;


    }
    public void HitBoxMelee3Deactivate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[2].SetActive(false);
        else meleeHitboxesUpgraded[2].SetActive(false);
        isMoving = false;

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
        }
    }


    public void EnablePlayermovement()
    {
        _pm.enabled = true;
    }

    public void DisablePlayerMovement()
    {
        _pm.enabled = false;
    }


    public void ColliderActivationRangedCombo(int ColliderNumber) //Tiene que ser distinto a la otra porque si no se bugea (xd moment)
    {
        //if (rangedHitboxes == null)
        //    return;

        shotGunParticleSystem.transform.position = gameObject.transform.position + (gameObject.transform.forward) * 2f + new Vector3(0, 2.5f, 0);
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
}