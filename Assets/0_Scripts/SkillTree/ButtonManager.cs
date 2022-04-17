using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    [Header("Imagenes de menu")]
    public GameObject canBuyAbility;
    public GameObject cantBuyAbility;

    [Header("Botones de imagenes de interacciones")]
    public GameObject noButton;
    public GameObject noButton2;
    public GameObject lastPressedButton;

    [SerializeField] private AbilitiesStatus _as;

    public List<Image> meleeImages = new List<Image>();
    public List<GameObject> rangedImages = new List<GameObject>();

    [SerializeField] private List<int> _meleeAbilitiesId = new List<int>();

    public List<Button> _rangedAbilitiesButtons = new List<Button>();
    [SerializeField] private List<int> _rangedAbilitiesId = new List<int>();


    public int meleeUpgrade;
    public int rangedUpgrade;


    [SerializeField] private SkillTree _st;



    private void Update()
    {
        
    }




    //Esta funcion es para interactura con el boton
    public void OnButtonSelectedRangedAbilities(int Id) //Al apretar el boton revisa la Id de la habilidad
    {
        //En esta funcion chequeo si puedo comprar o no la habilidad y si no la tengo comprada
        if (Input.GetButtonDown("Submit") && !_as.rangedAbilities[Id].isPurchased)
        {
            //Si me alcanzan los puntos para poder comprarlo, y estoy en el nivel correcto.
            if (_st.CurrentSkillPoints() >= _st.UpgradeSkillPointsNeeded() && Id < rangedUpgrade + 1)
            {

                Debug.Log("No accedi al menu de compra");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de can buy ability
                canBuyAbility.SetActive(true);
                //Activo el botton de no comprar por si me equivoque
                EventSystem.current.SetSelectedGameObject(noButton2);

            }
            else //Aca es si no tengo suficientes puntos me la deja comprar
            {
                Debug.Log("Accedi al menu de compra");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de cant buy ability
                cantBuyAbility.SetActive(true);
                //Le pongo como seleccion en el boton de no por si se equivoco
                EventSystem.current.SetSelectedGameObject(noButton);
            }
            
        }
        //Ahora, si la tengo comprada, Directamente la activo


        //Esto esta a hcequear porque podria activarla ni bien la compro pero veremos porque hay pasivas
        else if(Input.GetButtonDown("Submit") && _as.rangedAbilities[Id].isPurchased)
        {
            //Hago que el delegate ejecute la funcion que yo quiera ejecutar
            Debug.Log("Active la imagen de la habilidad");
            EventManager.Instance.Trigger("OnActivatingRangedAbilities", _rangedAbilitiesId[Id]);

            //Activo la imagen que le corresponda a ella
            for (int i = 0; i < rangedImages.Count; i++)
            {
                if (i == Id)
                    rangedImages[i].SetActive(true);
                else
                    rangedImages[i].SetActive(false);

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
            switch (rangedUpgrade) //Revisa que sea el primer caso.
            {
                case 0:
                    //Evento que gasta los skill poitns
                    EventManager.Instance.Trigger("OnSpendingSP", 3);
                    
                    //Me avisa que esta comprada para poder activarla
                    _as.rangedAbilities[rangedUpgrade].isPurchased = true;

                    //Me hace null la seleccion de boton
                    EventSystem.current.SetSelectedGameObject(null);
                    
                    //Desactiva las pantallas de opciones
                    canBuyAbility.SetActive(false);
                    cantBuyAbility.SetActive(false);

                    //Vuelve al ultimo boton apretado esto falta hotfixearlo igual
                    EventSystem.current.SetSelectedGameObject(lastPressedButton);

                    //Activo el boton para poder comprar la proxima, esto no me gusta quiero que tenga algo mas que se note
                    //_rangedAbilitiesButtons[0].interactable = true;

                    //Cambia al proximo caso
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

}
