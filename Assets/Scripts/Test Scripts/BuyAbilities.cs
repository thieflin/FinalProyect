using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAbilities : MonoBehaviour
{
    [SerializeField] private List<float> _skillCosts = new List<float>();
    [SerializeField] private List<int> _skillNumber = new List<int>();
    public void GetAbilityOne()
    {
        
        EventManager.Instance.Trigger("OnSpendingSP", _skillCosts[0], _skillNumber[0]);
    }
    public void GetAbilityTwo()
    {
        EventManager.Instance.Trigger("OnSpendingSP", _skillCosts[1], _skillNumber[1]);

    }
}
