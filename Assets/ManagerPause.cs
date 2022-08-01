using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    public GameObject resumeButton;

    public GameObject blackScreenBackground;

    public SkillTree skillTree;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause") && !skillTree.treeOpen)
        {
            if (!Pause.isPaused)
            {
                pauseMenu.SetActive(true);
                Pause.PauseGame();

                blackScreenBackground.SetActive(true);

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
                blackScreenBackground.SetActive(false);

            }
        }

    }
}
