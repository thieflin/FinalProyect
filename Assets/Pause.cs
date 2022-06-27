using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;

    public static void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public static void UnpauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
