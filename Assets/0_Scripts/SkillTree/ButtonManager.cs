using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public GameObject canBuyAbility;
    public GameObject cantBuyAbility;
    public GameObject noButton;
    public GameObject noButton2;
    public GameObject lastPressedButton;
    //[SerializeField] private List<float> _skillCosts = new List<float>();
    //[SerializeField] private List<int> _skillNumber = new List<int>();

    public List<Image> meleeImages = new List<Image>();
    public List<Image> rangedImages = new List<Image>();

    [SerializeField] private List<int> _meleeAbilities = new List<int>();
    [SerializeField] private List<int> _rangedAbilities = new List<int>();







    [SerializeField] private SkillTree _st;

    public void OnButtonSelected() //Interacciona con el boton de skill
    {
        //En esta funcion chequeo si puedo comprar o no la habilidad
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("si toy andando XD");
            //Si los puntos que tengo son menores a los que necesito para upgradear
            if (_st.CurrentSkillPoints() < _st.UpgradeSkillPointsNeeded())
            {
                Debug.Log("que odna puta entra aca xfavor");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de cant buy ability
                cantBuyAbility.SetActive(true);
                //Le selecciono el boton de back para que vuelva a lo anterior
                EventSystem.current.SetSelectedGameObject(noButton);
            }
            else //Aca es si tengo suficientes puntos
            {
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de can buy ability
                canBuyAbility.SetActive(true);
                //Le pongo como seleccion en el boton de no por si se equivoco
                EventSystem.current.SetSelectedGameObject(noButton2);
            }
        }
        
    }


    public void BackOrNoButton() //Vuelve para atras ya sea en compra o en no tener suficientes creditos
    {
        if (Input.GetButtonDown("Submit"))//Cuando apreto back o no
        {
            //Me des selecciona, me apaga los menus y me selecciona el ultimo boton apretado jee
            EventSystem.current.SetSelectedGameObject(null);
            canBuyAbility.SetActive(false);
            cantBuyAbility.SetActive(false);
            EventSystem.current.SetSelectedGameObject(lastPressedButton);
        }
    }

    public void PurchaseSkill1(int currentAbilityPurchase) //Compra el skill
    {
        
    }

    #region Melee skills
    public void SetMeleeSkillOne()
    {
        EventManager.Instance.Trigger("OnActivatingMeleeAbilities", _meleeAbilities[0]);
    }    
    
    public void SetMeleeSkillTwo()
    {
        EventManager.Instance.Trigger("OnActivatingMeleeAbilities", _meleeAbilities[1]);
    }

    #endregion

    #region Melee skills
    public void SetRangedSkillOne()
    {
        EventManager.Instance.Trigger("OnActivatingRangedAbilities", _rangedAbilities[0]);
    }

    public void SetRangedSkillTwo()
    {
        EventManager.Instance.Trigger("OnActivatingRangedAbilities", _rangedAbilities[1]);
    }

    #endregion

}
