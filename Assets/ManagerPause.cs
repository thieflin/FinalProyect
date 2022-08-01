using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public GameObject resumeButton;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Pause.isPaused);

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
            if (!Pause.isPaused)
            {
                pauseMenu.SetActive(true);
                Pause.PauseGame();

                EventSystem.current.SetSelectedGameObject(resumeButton);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else if(Pause.isPaused)
            {
                pauseMenu.SetActive(false);
                Pause.UnpauseGame();
                optionsMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                EventSystem.current.SetSelectedGameObject(null);
                Cursor.visible = false;

            }
        }

    }
}
