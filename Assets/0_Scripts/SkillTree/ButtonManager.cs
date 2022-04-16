using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{

    public void OnButtonSelected()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Debug.Log("toy seleccionando");
        }
    }
}
