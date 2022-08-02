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


    [Header("Desactivacion de movimiento")]
    public PlayerMovement pm;
    public Combo combo;

    public GameObject blackScreen;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            EventManager.Instance.Trigger("OnEarningSP", 15);
        }
        //Abre y cierra el skill tree con animacion
        //Consideron que esto no funcione si esta pausado el juego
        if (Input.GetKeyDown(KeyCode.CapsLock) || Input.GetButtonDown("SkillTree") && !Pause.isPaused)
        {
            if (treeOpen)
            {
                openingAnimation.SetTrigger("Close");
                EventSystem.current.SetSelectedGameObject(null);
                StartCoroutine(DeActivateBlackbackground());
                treeOpen = false;
                combo.enabled = true;
                pm.enabled = true;
            }
                
            else
            {
                EventSystem.current.SetSelectedGameObject(startingButton);
                openingAnimation.SetTrigger("Open");
                treeOpen = true;
                StartCoroutine(ActivateBlackbackground());
                combo.enabled = false;
                pm.enabled = false;
            }
        }
    }

    IEnumerator ActivateBlackbackground()
    {
        yield return new WaitForSeconds(.5f);
        blackScreen.SetActive(true);
    }
    IEnumerator DeActivateBlackbackground()
    {
        yield return new WaitForSeconds(.5f);
        blackScreen.SetActive(false);
    }



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

    public float CurrentSkillPoints()
    {
        return _skillPoints;
    }
    public float UpgradeSkillPointsNeeded()
    {
        return _upgradeSkillPoints;
    }
}
