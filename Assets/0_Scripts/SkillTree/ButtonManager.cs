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

    [SerializeField] private List<Button> _meleeAbilityButtons = new List<Button>();
    [SerializeField] private List<Button> _rangedAbilityButtons = new List<Button>();

    public List<Image> meleeImages = new List<Image>();
    public List<Image> rangedImages = new List<Image>();

    [SerializeField] private List<int> _meleeAbilitiesId = new List<int>();
    [SerializeField] private List<int> _rangedAbilitiesId = new List<int>();


    public int meleeUpgrade;
    public int rangedUpgrade;




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

    public void PurchaseSkillOneMelee() //Compra el skill
    {
        meleeUpgrade++;
        
        //Al comprarlo tengo que ponerle el iconito mas bonito para despues setearlo
        //Tambien tengo que desactivar al boton para que ya no moleste mas o marcarlo de alguna forma que se vea bien

    }

    public void PurchaseSkillTwoMelee()
    {
        //Esto es a chequear igual porque simplemente podria activar el otro boton y seria mas facil 
        if(meleeUpgrade > 1)
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
        EventManager.Instance.Trigger("OnActivatingMeleeAbilities", _meleeAbilitiesId[0]);
        //Al setearlo tengo que activarle la imagen correspondiente
    }    
    
    public void SetMeleeSkillTwo()
    {

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
