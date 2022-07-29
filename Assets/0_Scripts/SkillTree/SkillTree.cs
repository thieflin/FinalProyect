using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SkillTree : MonoBehaviour
{

    public int _skillPoints = 0;
    public TextMeshProUGUI _skillPointsText;
    public int _upgradeSkillPoints = 0;
    public GameObject canBuyImage;
    public GameObject cantBuyImage;
    public GameObject startingButton;

    public Animator openingAnimation;
    public bool treeOpen;

    
    
    private void Update()
    {
        
        //Abre y cierra el skill tree con animacion
        if (Input.GetKeyDown(KeyCode.CapsLock) || Input.GetButtonDown("SkillTree"))
        {
            if (treeOpen)
            {
                openingAnimation.SetTrigger("Close");
                treeOpen = false;
            }
                
            else
            {
                EventSystem.current.SetSelectedGameObject(startingButton);
                openingAnimation.SetTrigger("Open");
                treeOpen = true;
            }
        }
    }

    //Event Animation boolean para no spamear el open y close del skill tree

    private void Start()
    {
        EventManager.Instance.Subscribe("OnEarningSP", EarningSp);
        EventManager.Instance.Subscribe("OnSpendingSP", UpgrandingAbility);
        //EventManager.Instance.Subscribe("OnObtainingBlueprint", BluePrintActivations);
        treeOpen = false;

        _skillPointsText.text = _skillPoints.ToString();
    }

    private void EarningSp(params object[] parameters) // Obtiene SP
    {
        _skillPoints += (int)parameters[0];
        _skillPointsText.text = _skillPoints.ToString();
        //EventManager.Instance.Trigger("OnUpdatingSp", _skillPoints);
        Debug.Log(_skillPoints);
    }

    private void UpgrandingAbility(params object[] parameters) //Usa los SP
    {
        _skillPoints -= (int)parameters[0];//Le saco los skillpoitns que cueste la habilidad
        _skillPointsText.text = _skillPoints.ToString();
    }

    //private void BluePrintActivations(params object[] parameters) //Me activa el skill que yo compre (visualmente)
    //{
    //    for (int i = 0; i < _bluePrintImages.Count; i++)
    //    {
    //        _bluePrintImages[(int)parameters[0]].enabled = true;
    //        var tempColor = _bluePrintImages[(int)parameters[0]].color;
    //        tempColor.a = 255;
    //        _bluePrintImages[(int)parameters[0]].color = tempColor;
    //    }
    //}

    public float CurrentSkillPoints()
    {
        return _skillPoints;
    }
    public float UpgradeSkillPointsNeeded()
    {
        return _upgradeSkillPoints;
    }
}
