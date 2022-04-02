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
    public float chargeAttack;
    public float chargeAttackTimer;
    public Animator anim;
    public bool canAttack = true;
    public float comboCd, comboFixedCdForAtt1;
    [Header("Variables para interceptar el spam en los combos")] //Los timers son los que bajan, los time son cuanto dura
    [SerializeField] private float firstAttackTimer, firstAttackTime;
    [SerializeField] private float secondAttackTimer, secondAttackTime;
    [SerializeField] private float finisherMeleeTimer, finisherMeleeTime;
    [SerializeField] private bool canDoAtt1, canDoAtt2, canDoAtt3 = false;


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0)) //Cuando apreto el mouse y 
        {
            if (canAttack)
            {
                anim.SetTrigger("A1");
                canAttack = false;
            }
            else mouseDown0 = 1;

        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
             mouseDown0 = 2;

        }


        //if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        //{
        //    if (comboCounter == 0)
        //    {
        //        anim.SetTrigger("A1");
        //        Debug.Log("Ataque numero 1 del combo");
        //        comboCounter++;
        //        isComboing = true;
        //    }
        //    else if (comboCounter == 1 && !Input.GetKey(KeyCode.LeftShift))
        //    {
        //        Debug.Log("Ataque numero 2 del combo");
        //        comboCounter++;
        //        anim.SetTrigger("A2");
        //        isComboing = true;
        //    }
        //    //else if (comboCounter == 1 && Input.GetKey(KeyCode.LeftShift))
        //    //{
        //    //    comboCounter++;
        //    //    Debug.Log("Ataque numero 2 finisher pesado pesado");
        //    //    Debug.Log("Finisher with melee");
        //    //    comboEnded = true;
        //    //}
        //    else if (comboCounter == 2)
        //    {
        //        comboCounter++;
        //        anim.SetTrigger("A3");
        //        Debug.Log("Ataque numero 3 del combo rapido");
        //        Debug.Log("Finisher with melee");
        //        canAttack = false;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    if (comboCounter == 2 && !Input.GetKey(KeyCode.LeftShift) && canAttack)
        //    {
        //        comboCounter++;
        //        anim.SetTrigger("G3");
        //        Debug.Log("Ataque numero 3 del combo");
        //        Debug.Log("Finisher with shotgun regular");
        //        canAttack = false;
        //    }
        //    else if (comboCounter == 2 && Input.GetKey(KeyCode.LeftShift))
        //    {
        //        Debug.Log("Ataque numero 3 del combo");
        //        comboCounter++;
        //        comboCounter++; Debug.Log("Heavy Finisher");
        //        isComboing = true;
        //    }
        //}

        if (isComboing) //Lo que dura el combo
        {
            comboTime -= Time.deltaTime;
        }

        if (!canAttack) //Para poder spamear sin problemas
        {
            comboCd -= Time.deltaTime;
            if (comboCd < 0)
            {

                canAttack = true;
                comboCounter = 0;
                comboCd = comboFixedCdForAtt1;
            }
        }


        if (comboTime <= 0) //Se encarga de que el combo se reinicie si no ataca lo suficientemente rapido
        {
            comboCounter = 0;
            isComboing = false;
            anim.SetBool("CanAtt", false);
            comboTime = comboUptime;
        }

        //FirstAttackLimiter();
        //SecondAttackLimiter();
    }
    void FirstAttackLimiter()
    {
        if (canDoAtt1) //Para poder spamear sin problemas
        {
            firstAttackTimer -= Time.deltaTime;
            if (comboCd < 0)
            {
                firstAttackTimer = firstAttackTime;
                canDoAtt2 = true;
            }
        }
    }

    void SecondAttackLimiter()
    {
        if (canDoAtt2) //Para poder spamear sin problemas
        {
            secondAttackTimer -= Time.deltaTime;
            if (comboCd < 0)
            {
                secondAttackTimer = secondAttackTime;
                canDoAtt2 = true;
            }
        }
    }

    public int mouseDown0;

    public void SelectNextComboAnmiation()
    {
        Debug.Log("entre XD");
        switch (mouseDown0)
        {
            case 0:
                Debug.Log("entre XD0");

                break;
            case 1:
                {
                    anim.SetTrigger("A1");
                    Debug.Log("entre XD1");
                    mouseDown0 = 0;
                }
                break;
            case 2:
                {
                    anim.SetTrigger("A2");
                    Debug.Log("entre XD2");
                    mouseDown0 = 0;
                }
                break;
        }
    }
    void ExecuteCombo()

    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack) //Cuando apreto el mouse y 
        {
            if (comboCounter == 0)
            {
                anim.SetTrigger("A1");
                Debug.Log("Ataque numero 1 del combo");
                comboCounter++;
                canDoAtt1 = true;
                isComboing = true;
            }
            else if (comboCounter == 1 && !Input.GetKey(KeyCode.LeftShift) && canDoAtt1)
            {
                Debug.Log("Ataque numero 2 del combo");
                comboCounter++;
                anim.SetTrigger("A2");
                isComboing = true;

            }
            //else if (comboCounter == 1 && Input.GetKey(KeyCode.LeftShift))
            //{
            //    comboCounter++;
            //    Debug.Log("Ataque numero 2 finisher pesado pesado");
            //    Debug.Log("Finisher with melee");
            //    comboEnded = true;
            //}
            else if (comboCounter == 2 && canDoAtt2)
            {
                comboCounter++;
                anim.SetTrigger("A3");
                Debug.Log("Ataque numero 3 del combo rapido");
                Debug.Log("Finisher with melee");
                canAttack = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (comboCounter == 2 && !Input.GetKey(KeyCode.LeftShift) && canAttack)
            {
                comboCounter++;
                anim.SetTrigger("G3");
                Debug.Log("Ataque numero 3 del combo");
                Debug.Log("Finisher with shotgun regular");
                canAttack = false;
            }
            else if (comboCounter == 2 && Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("Ataque numero 3 del combo");
                comboCounter++;
                comboCounter++; Debug.Log("Heavy Finisher");
                isComboing = true;
            }
        }

    }
}
