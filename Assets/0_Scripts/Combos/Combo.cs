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
    public bool upgradedHitbox;
    private PlayerMovement _pm;
    public static int swordDmg;

    private void Start()
    {
        swordDmg = 10;
        upgradedHitbox = false;
        EventManager.Instance.Subscribe("OnGettingBiggerHitbox", UpgradedHitboxTrue);
        _pm = GetComponent<PlayerMovement>();

    }
    void Update()
    {
        InputController();
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
                _pm._movementSpeed = 10;
                ani.SetTrigger("A1");
            }
            else
                _nextCombo = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
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



                _isIdle = true;
                break;
            case 1:
                _pm._movementSpeed = 10;
               

                ani.SetTrigger("A1");

                _nextCombo = 0;
                break;
            case 2:
                _pm._movementSpeed = 10;
               

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
    }

    public void HitBoxMelee1Activate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[0].SetActive(true);
        else meleeHitboxesUpgraded[0].SetActive(true);
    }


    public void HitBoxMelee2Activate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[1].SetActive(true);
        else meleeHitboxesUpgraded[1].SetActive(true);

    }

    public void HitBoxMelee3Activate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[2].SetActive(true);
        else meleeHitboxesUpgraded[2].SetActive(true);

    }

    public void HitBoxMelee1Deactivate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[0].SetActive(false);
        else meleeHitboxesUpgraded[0].SetActive(false);

    }
    public void HitBoxMelee2Deactivate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[1].SetActive(false);
        else meleeHitboxesUpgraded[1].SetActive(false);

    }
    public void HitBoxMelee3Deactivate()
    {
        if (!upgradedHitbox)
            meleeHitboxes[2].SetActive(false);
        else meleeHitboxesUpgraded[2].SetActive(false);
    }
}