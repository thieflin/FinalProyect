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
<<<<<<< HEAD
    public Animator anim;
    public bool canAttack =true;
    public float comboDowntime, comboDowntimeFixedValue;
=======
    public float chargeAttack;
    public float chargeAttackTimer;
>>>>>>> parent of 4153ca6 (Combo 1 animations falta cortarlo)



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            if (comboCounter == 0)
            {
                Debug.Log("Ataque numero 1 del combo");
                comboCounter++;
                isComboing = true;
            }
            else if (comboCounter == 1 && !Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Ataque numero 2 del combo");
                comboCounter++;
                isComboing = true;
            }
<<<<<<< HEAD
=======
            else if (comboCounter == 1 && Input.GetKey(KeyCode.LeftShift))
            {
                comboCounter++;
                Debug.Log("Ataque numero 2 finisher pesado pesado");
                Debug.Log("Finisher with melee");
                comboEnded = true;
            }
>>>>>>> parent of 4153ca6 (Combo 1 animations falta cortarlo)
            else if (comboCounter == 2)
            {
                comboCounter++;
                Debug.Log("Ataque numero 3 del combo rapido");
                Debug.Log("Finisher with melee");
<<<<<<< HEAD
=======
                comboEnded = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(comboCounter == 2 && !Input.GetKey(KeyCode.LeftShift))
            {
                comboCounter++;
                Debug.Log("Ataque numero 3 del combo");
                Debug.Log("Finisher with shotgun regular");
>>>>>>> parent of 4153ca6 (Combo 1 animations falta cortarlo)
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
