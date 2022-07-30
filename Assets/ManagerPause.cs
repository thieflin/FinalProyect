using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Pause.isPaused);

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if (!Pause.isPaused)
            {
                Debug.Log("ENTRE ACA TAMBIEN");
                pauseMenu.SetActive(true);
                Pause.PauseGame();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else if(Pause.isPaused)
            {
                Debug.Log("ENTRE ACA");
                pauseMenu.SetActive(false);
                Pause.UnpauseGame();
                optionsMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }

    }
}
