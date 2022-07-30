using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPause : MonoBehaviour
{
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause.isPaused)
            {
                pauseMenu.SetActive(false);
                Pause.UnpauseGame();
            }
            else
            {
                pauseMenu.SetActive(true);
                Pause.PauseGame();
            }
        }

    }
}
