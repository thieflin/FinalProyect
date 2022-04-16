using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{

    public float _skillPoints = 0;
    public float _upgradeSkillPoints = 0;
    [SerializeField] private GameObject _skillTree = null;
    [SerializeField] private List<Image> _bluePrintImages = null;
    public GameObject canBuyImage;
    public GameObject cantBuyImage;

    private void Awake()
    {
        _skillTree.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
            if (_skillTree.activeSelf)
                _skillTree.SetActive(false);
            else _skillTree.SetActive(true);
        Debug.Log(_skillPoints);
    }

    private void Start()
    {
        EventManager.Instance.Subscribe("OnEarningSP", EarningSp);
        EventManager.Instance.Subscribe("OnSpendingSP", UpgrandingAbility);
        EventManager.Instance.Subscribe("OnObtainingBlueprint", BluePrintActivations);
    }

    private void EarningSp(params object[] parameters) // Obtiene SP
    {
        _skillPoints += (float)parameters[0];
        EventManager.Instance.Trigger("OnUpdatingSp", _skillPoints);
        Debug.Log(_skillPoints);
    }

    private void UpgrandingAbility(params object[] parameters) //Usa los SP
    {
        _skillPoints -= (float)parameters[0];//Le saco los skillpoitns que cueste la habilidad
       
        EventManager.Instance.Trigger("OnEnablingNewAbility", (int)parameters[1]);

    }


    private void AdquireSkillAttempt(params object[] parameters)
    {
        var spSpent = (float)parameters[0];
        if (_skillPoints < spSpent)
        {

        }
        else
        {

        }
    }






    private void BluePrintActivations(params object[] parameters) //Me activa el skill que yo compre (visualmente)
    {
        for (int i = 0; i < _bluePrintImages.Count; i++)
        {
            _bluePrintImages[(int)parameters[0]].enabled = true;
            var tempColor = _bluePrintImages[(int)parameters[0]].color;
            tempColor.a = 255;
            _bluePrintImages[(int)parameters[0]].color = tempColor;
        }
    }

    public float CurrentSkillPoints()
    {
        return _skillPoints;
    }
    public float UpgradeSkillPointsNeeded()
    {
        return _upgradeSkillPoints;
    }
}
