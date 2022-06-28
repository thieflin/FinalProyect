using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject startButton;

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            EventSystem.current.SetSelectedGameObject(null);
            menu.SetActive(!menu.activeSelf); //SI ESTA DESACTIVADO SE ACTIVA Y SI ESTA ACTIVADO SE DESACTIVA
            EventSystem.current.SetSelectedGameObject(startButton);
        }

        if (menu.activeSelf)
        {
            Pause.PauseGame();
        }
        else
        {
            Pause.UnpauseGame();
        }
    }
}
