using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    bool _isIdle = true;
    public Animator ani;
    int _nextCombo = 0;

    void Update()
    {
        InputController();
    }

    void InputController()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
}