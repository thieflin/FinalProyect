using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static void PauseGame()
    {
        Time.timeScale = 0.0000001f;
    }

    public static void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
