using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesStatus : MonoBehaviour
{
    public List<Abilities> abilities = new List<Abilities>();
    void Start()
    {
        EventManager.Instance.Subscribe("OnEnablingNewAbility", ActivateAbility);
    }

    public void ActivateAbility(params object[] parameters)//Me lo hace true a la que yo quiera
    {
        Debug.Log("Toy Working");
        abilities[(int)parameters[0]].isActive = true;
    }
    
}
