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



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (comboCounter == 0)
            {
                Debug.Log("Ataque numero 1 del combo");
                comboCounter++;
                isComboing = true;
            }
            else if (comboCounter == 1)
            {
                Debug.Log("Ataque numero 2 del combo");
                comboCounter++;
                isComboing = true;
            }
            else if (comboCounter == 2)
            {
                comboCounter++;
                Debug.Log("Ataque numero 3 del combo");
            }
            else if (comboCounter == 3)
            {
                comboCounter++;
                Debug.Log("Ataque numero 4 del combo");
                Debug.Log("Finisher with melee");
                comboEnded = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(comboCounter == 3)
            {
                comboCounter++;
                Debug.Log("Ataque numero 4 del combo");
                Debug.Log("Finisher with shotgun");
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
