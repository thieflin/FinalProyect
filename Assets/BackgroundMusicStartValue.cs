using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusicStartValue : MonoBehaviour
{
    void Start()
    {
        GetComponent<Slider>().value = BackgroundMusic.backgroundMusicValue;
    }

}
