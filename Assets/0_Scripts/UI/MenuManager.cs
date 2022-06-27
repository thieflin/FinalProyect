using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf); //SI ESTA DESACTIVADO SE ACTIVA Y SI ESTA ACTIVADO SE DESACTIVA
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
