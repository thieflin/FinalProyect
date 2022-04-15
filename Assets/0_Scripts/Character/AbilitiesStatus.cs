using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesStatus : MonoBehaviour
{
    public List<Abilities> abilities = new List<Abilities>();
    public List<Abilities> abilitiesInUsage = new List<Abilities>();
    private int _abilityId;
    Abilities _currentAbility;
    public int dmg;
    public int cd;

    AbilityMeleeOne am1;

    public delegate void AbilityPicker();
    AbilityPicker a1;    
    AbilityPicker a2;

    void Start()
    {
        am1 = new AbilityMeleeOne(dmg , cd);
        a1 = am1.OnUpdate;


        EventManager.Instance.Subscribe("OnEnablingNewAbility", ActivateAbility);
        for (int i = 0; i < abilities.Count; i++)
        {
            abilities[i].abilityId = i;
        }
    }
    //Apreto un boton de seteo
    //a1 = am2.OnUpdate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            a1();
        }



        if (Input.GetButtonDown("AbilitySwitch"))
        {
            if (_abilityId == 0)
            {
                _currentAbility = abilitiesInUsage[_abilityId];
            }
            else if(_abilityId == 1)
            {
                _currentAbility = abilitiesInUsage[_abilityId];
            }
        
        }
    }

    public void ActivateAbility(params object[] parameters)//Me lo hace true a la que yo quiera
    {
        Debug.Log("Toy Working");
        abilities[(int)parameters[0]].isActive = true;
    }
    
}
