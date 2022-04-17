using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesStatus : MonoBehaviour
{
    public List<Abilities> rangedAbilities = new List<Abilities>();
    public List<Abilities> meleeAbilities = new List<Abilities>();
    public List<int> dmg = new List<int>();


    public PlayerMovement pm;

    public static Action currentMeleeAbility;
    public static Action currentRangedAbility;

    void Start()
    {
        currentMeleeAbility = Debugchan;
        currentRangedAbility = Debugchan;
        EventManager.Instance.Subscribe("OnActivatingMeleeAbilities", SetMeleeAbility);
        EventManager.Instance.Subscribe("OnActivatingRangedAbilities", SetRangedAbility);
    }
    //Apreto un boton de seteo
    //a1 = am2.OnUpdate;
    public void Debugchan()
    {
        Debug.Log("habilidad no existente");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("RangedSkill"))
        {
            currentRangedAbility();
        }
        else if (Input.GetKeyDown(KeyCode.E)|| Input.GetButtonDown("MeleeSkill"))
        {
            currentMeleeAbility();
        }
    }

    public void ActivateAbility(params object[] parameters)//Me lo hace true a la que yo quiera
    {
        Debug.Log("Toy Working");
    }
    
    //Esto basicamente lo que haces es definirme que on update voy a usar
    public void SetMeleeAbility(params object[] parameters)
    {
        currentMeleeAbility = meleeAbilities[(int)parameters[0]].OnUpdate;
    }

    //Esto me define que on update voy a usar pero me lo ddefine para las habilidades que son ranged
    public void SetRangedAbility(params object[] parameters)
    {
        currentRangedAbility = rangedAbilities[(int)parameters[0]].OnUpdate;
    }

    public void EnablePM()
    {
        pm.enabled = true;
    }

    public void DisablePM()
    {
        pm.enabled = false;
    }
}
