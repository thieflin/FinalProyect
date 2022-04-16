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

    public static Action currentMeleeAbility;
    public static Action currentRangedAbility;

    void Start()
    {
        //currentMeleeAbility = meleeAbilities[0].OnUpdate;
        //currentRangedAbility = rangedAbilities[0].OnUpdate;
        EventManager.Instance.Subscribe("OnActivatingMeleeAbilities", SetMeleeAbility);
        EventManager.Instance.Subscribe("OnActivatingRangedAbilities", SetRangedAbility);
    }
    //Apreto un boton de seteo
    //a1 = am2.OnUpdate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentRangedAbility();
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
}
