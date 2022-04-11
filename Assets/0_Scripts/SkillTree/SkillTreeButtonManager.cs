using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeButtonManager : MonoBehaviour, ISkillPointDrop
{
    [Header("Vars associated to upgrading max healthpoints")]
    [SerializeField] private int _hpAgument;
    [SerializeField] private float _hpCost;
    [SerializeField] private float _hpCostAgument;
    [SerializeField] private float _maxHpCost;
    [SerializeField] private float _sp;
    public SkillTree st;
    public void SkillOne()
    {
        EventManager.Instance.Trigger("OnSkillActivation", 0);
    }

    public void GetHP()
    {
        if (_hpCost < _maxHpCost && st.CurrentSkillPoints() - _hpCost > 0)
        {
            EventManager.Instance.Trigger("OnSpendingSP", _hpCost);
            EventManager.Instance.Trigger("OnIncreasingHp", _hpAgument);
            _hpCost = _hpCost * _hpCostAgument;
        }
        else Debug.Log("You have the maximum ammount of Hit Points or you dont have enough points to lvl up the HP");
    }

    public void GetSp()
    {
        EventManager.Instance.Trigger("OnEarningSP", _sp);
    }
}
