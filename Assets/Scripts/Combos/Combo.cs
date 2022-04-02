using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public int comboCounter;
    public float comboUptime;
    public float comboTime;
    public bool isComboing;
    public bool comboEnded;
    public Animator anim;
    public bool canAttack =true;
    public float comboDowntime, comboDowntimeFixedValue;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            if (comboCounter == 0)
            {
               // anim.SetBool("Att1", true);
                anim.SetTrigger("A1");
                Debug.Log("Ataque numero 1 del combo");
                comboCounter++;
                isComboing = true;
            }
            else if (comboCounter == 1 && !Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Ataque numero 2 del combo");
                comboCounter++;
                anim.SetTrigger("A2");
                isComboing = true;
            }
            else if (comboCounter == 2)
            {
                comboCounter++;
                anim.SetTrigger("A3");
                Debug.Log("Ataque numero 3 del combo rapido");
                Debug.Log("Finisher with melee");
                comboEnded = true;
            }
        }

        if (isComboing)
        {
            comboTime -= Time.deltaTime;
        }

        if (comboTime <= 0)
        {
            comboCounter = 0;
            isComboing = false;
            comboTime = comboUptime;
        }

        if (comboEnded)
        {
            isComboing = false;
            comboCounter = 0;
            comboEnded = false;
            comboTime = comboUptime;
        }

    }
}
