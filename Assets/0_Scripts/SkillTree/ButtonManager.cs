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
        if (Input.GetButtonDown("Submit"))
        {
            EventSystem.current.SetSelectedGameObject(null);
            canBuyAbility.SetActive(false);
            cantBuyAbility.SetActive(false);
            EventSystem.current.SetSelectedGameObject(lastPressedButton);
        }
    }

    public void PurchaseSkill() //Compra el skill
    {
        

    }



}
