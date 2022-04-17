using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    [Header("Imagenes de menu de ranged")]
    public GameObject canBuyAbility;
    public GameObject cantBuyAbility;

    [Header("Imagenes de menu de melee")]
    public GameObject canBuyAbilityMelee;
    public GameObject cantBuyAbilityMelee;

    [Header("Botones de imagenes de interacciones de ranged")]
    public GameObject noButton;
    public GameObject noButton2;    
    public GameObject backButtonMele;
    public GameObject noButtonMelee; 
    public GameObject lastPressedButton;

    [SerializeField] private AbilitiesStatus _as;

    [Header("Lista de las imagenes de la UI para activarlas en el skill tree")]
    public List<GameObject> meleeImages = new List<GameObject>();
    public List<GameObject> rangedImages = new List<GameObject>();

    [Header("Lista de las ID de las habilidades, tienen que ser similares a las que ya estan")]
    [SerializeField] private List<int> _meleeAbilitiesId = new List<int>();
    [SerializeField] private List<int> _rangedAbilitiesId = new List<int>();


    public int meleeUpgrade;
    public int rangedUpgrade;
    public Button button;

    [SerializeField] private SkillTree _st;

    #region RangedAbilities
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

            //Activo la imagen que le corresponda a ella (esto se podria mejorar y pasarlo en el delegate directamente pero esas
            //upgrades se ven despues, rpimero que ande)
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
    #endregion

    #region MeleeAbilities
    public void OnButtonSelectedMeleeAbilities(int Id) //Al apretar el boton revisa la Id de la habilidad
    {
        //En esta funcion chequeo si puedo comprar o no la habilidad y si no la tengo comprada
        if (Input.GetButtonDown("Submit") && !_as.meleeAbilities[Id].isPurchased)
        {
            //Si me alcanzan los puntos para poder comprarlo, y estoy en el nivel correcto.
            if (_st.CurrentSkillPoints() >= _st.UpgradeSkillPointsNeeded() && Id < meleeUpgrade + 1)
            {

                Debug.Log("Accedi al menu de compra");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de can buy ability
                canBuyAbilityMelee.SetActive(true);
                //Activo el botton de no comprar por si me equivoque
                EventSystem.current.SetSelectedGameObject(noButtonMelee);

            }
            else //Aca es si no tengo suficientes puntos me la deja comprar
            {
                Debug.Log("No accedi al menu de compra");
                //Desselecciono el boton anterior
                EventSystem.current.SetSelectedGameObject(null);
                //Activo el menu de cant buy ability
                cantBuyAbilityMelee.SetActive(true);
                //Le pongo como seleccion en el boton de no por si se equivoco
                EventSystem.current.SetSelectedGameObject(backButtonMele);
            }

        }
        //Ahora, si la tengo comprada, Directamente la activo


        //Esto esta a hcequear porque podria activarla ni bien la compro pero veremos porque hay pasivas
        else if (Input.GetButtonDown("Submit") && _as.meleeAbilities[Id].isPurchased)
        {
            //Hago que el delegate ejecute la funcion que yo quiera ejecutar
            Debug.Log("Active la imagen de la habilidad");
            EventManager.Instance.Trigger("OnActivatingMeleeAbilities", _meleeAbilitiesId[Id]);

            //Activo la imagen que le corresponda a ella
            for (int i = 0; i < meleeImages.Count; i++)
            {
                if (i == Id)
                    meleeImages[i].SetActive(true);
                else
                    meleeImages[i].SetActive(false);

            }
        }

    }


    public void BackOrNoButtonMelee() //Vuelve para atras ya sea en compra o en no tener suficientes creditos
    {
        if (Input.GetButtonDown("Submit"))//Cuando apreto back o no
        {
            //Me des selecciona, me apaga los menus y me selecciona el ultimo boton apretado jee
            EventSystem.current.SetSelectedGameObject(null);
            cantBuyAbilityMelee.SetActive(false);
            canBuyAbilityMelee.SetActive(false);
            EventSystem.current.SetSelectedGameObject(lastPressedButton);
        }
    }

    public void PurchaseSkillMelee() //Compra el skill
    {
        if (Input.GetButtonDown("Submit"))
        {
            switch (meleeUpgrade) //Revisa que sea el primer caso.
            {
                case 0:
                    //Evento que gasta los skill poitns
                    EventManager.Instance.Trigger("OnSpendingSP", 3);

                    //Me avisa que esta comprada para poder activarla
                    _as.meleeAbilities[meleeUpgrade].isPurchased = true;

                    //Me hace null la seleccion de boton
                    EventSystem.current.SetSelectedGameObject(null);

                    //Desactiva las pantallas de opciones
                    cantBuyAbilityMelee.SetActive(false);
                    canBuyAbilityMelee.SetActive(false);

                    //Vuelve al ultimo boton apretado esto falta hotfixearlo igual
                    EventSystem.current.SetSelectedGameObject(lastPressedButton);

                    //Activo el boton para poder comprar la proxima, esto no me gusta quiero que tenga algo mas que se note
                    //_rangedAbilitiesButtons[0].interactable = true;

                    //Cambia al proximo caso
                    meleeUpgrade++;
                    break;
                case 1:
                    EventManager.Instance.Trigger("OnSpendingSP", 3);
                    _as.meleeAbilities[meleeUpgrade].isPurchased = true;
                    EventSystem.current.SetSelectedGameObject(null);
                    cantBuyAbilityMelee.SetActive(false);
                    canBuyAbilityMelee.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(lastPressedButton);
                    meleeUpgrade++;
                    break;
            }

            Debug.Log("Compre la habilidad");


        }

        //Al comprarlo tengo que ponerle el iconito mas bonito para despues setearlo
        //Tambien tengo que desactivar al boton para que ya no moleste mas o marcarlo de alguna forma que se vea bien

    }
    #endregion
}
