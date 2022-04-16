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

    [SerializeField] private AbilitiesStatus _as;

    public List<Image> meleeImages = new List<Image>();
    public List<Image> rangedImages = new List<Image>();

    [SerializeField] private List<int> _meleeAbilitiesId = new List<int>();
    [SerializeField] private List<int> _rangedAbilitiesId = new List<int>();


    public int meleeUpgrade;
    public int rangedUpgrade;




    [SerializeField] private SkillTree _st;

    public void OnButtonSelectedRangedAbilities(int Id) //Interacciona con el boton de skill
    {
        //En esta funcion chequeo si puedo comprar o no la habilidad y si no la tengo comprada
        if (Input.GetButtonDown("Submit") && !_as.rangedAbilities[Id].isPurchased)
        {
            //Si los puntos que tengo son menores a los que necesito para upgradear
            if (_st.CurrentSkillPoints() < _st.UpgradeSkillPointsNeeded())
            {
                Debug.Log("No accedi al menu de compra");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de cant buy ability
                cantBuyAbility.SetActive(true);
                //Le selecciono el boton de back para que vuelva a lo anterior
                EventSystem.current.SetSelectedGameObject(noButton);
            }
            else //Aca es si tengo suficientes puntos
            {
                Debug.Log("Accedi al menu de compra");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de can buy ability
                canBuyAbility.SetActive(true);
                //Le pongo como seleccion en el boton de no por si se equivoco
                EventSystem.current.SetSelectedGameObject(noButton2);
            }
            
        }
        else if(Input.GetButtonDown("Submit") && _as.rangedAbilities[Id].isPurchased)
        {
            //Hago que el delegate ejecute la funcion que yo quiera ejecutar
            Debug.Log("Active la imagen de la habilidad");
            EventManager.Instance.Trigger("OnActivatingRangedAbilities", _rangedAbilitiesId[Id]);

            //Activo la imagen que le corresponda a ella
            for (int i = 0; i < rangedImages.Count; i++)
            {
                if (i == Id)
                    rangedImages[i].enabled = true;
                else
                    rangedImages[i].enabled = false;
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

    public void PurchaseSkillRanged() //Compra el skill
    {
        if (Input.GetButtonDown("Submit"))
        {
            switch (rangedUpgrade)
            {
                case 0:
                    EventManager.Instance.Trigger("OnSpendingSP", 3);
                    _as.rangedAbilities[rangedUpgrade].isPurchased = true;
                    EventSystem.current.SetSelectedGameObject(null);
                    canBuyAbility.SetActive(false);
                    cantBuyAbility.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(lastPressedButton);
                    rangedUpgrade++;
                    break;
                case 1:
                    EventManager.Instance.Trigger("OnSpendingSP", 3);
                    _as.rangedAbilities[rangedUpgrade].isPurchased = true;
                    EventSystem.current.SetSelectedGameObject(null);
                    canBuyAbility.SetActive(false);
                    cantBuyAbility.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(lastPressedButton);
                    rangedUpgrade++;
                    break;
            }
                
            Debug.Log("Compre la habilidad");
            
            
        }

        //Al comprarlo tengo que ponerle el iconito mas bonito para despues setearlo
        //Tambien tengo que desactivar al boton para que ya no moleste mas o marcarlo de alguna forma que se vea bien

    }

    public void PurchaseSkillTwoMelee()
    {
        //Esto es a chequear igual porque simplemente podria activar el otro boton y seria mas facil 
        if (meleeUpgrade > 1)
        {

        }
        else
        {
            //Hago el cartelito de no podes comprar porque no desbloqueaste el nivel anterior
        }
    }

    #region Melee skills
    public void SetMeleeSkillOne()
    {
        
        //Al setearlo tengo que activarle la imagen correspondiente
    }

    public void SetMeleeSkillTwo()
    {
        if (Input.GetButtonDown("Cancel"))
            Debug.Log("pls funjciona");
        //EventManager.Instance.Trigger("OnActivatingMeleeAbilities", _meleeAbilitiesId[1]);
    }

    #endregion

    #region Melee skills
    public void SetRangedSkillOne()
    {
        EventManager.Instance.Trigger("OnActivatingRangedAbilities", _rangedAbilitiesId[0]);
    }

    public void SetRangedSkillTwo()
    {
        EventManager.Instance.Trigger("OnActivatingRangedAbilities", _rangedAbilitiesId[1]);
    }

    #endregion

}
