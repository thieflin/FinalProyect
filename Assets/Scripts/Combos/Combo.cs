using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    bool _isIdle = true;
    public Animator ani;
    int _nextCombo = 0;
    public List<GameObject> meleeHitboxes = new List<GameObject>();
    public List<GameObject> meleeHitboxesUpgraded = new List<GameObject>();
    public bool upgradedHitbox;

    private void Start()
    {
        upgradedHitbox = false;
        EventManager.Instance.Subscribe("OnGettingBiggerHitbox", UpgradedHitboxTrue);

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
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Fire1"))
        {
            if (_isIdle == true)
            {
                _isIdle = false;
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
                _isIdle = true;
                break;
            case 1:
                ani.SetTrigger("A1");
                _nextCombo = 0;
                break;
            case 2:
                ani.SetTrigger("A2");
                _nextCombo = 0;
                break;
        }
    }

    public void FinishCombo()
    {
        _nextCombo = 0;
        ani.SetTrigger("Idle");
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