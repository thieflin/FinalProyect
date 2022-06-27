using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    public static float backgroundMusicValue;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void UpdateValue(float value)
    {
        backgroundMusicValue = value;
    }
}
