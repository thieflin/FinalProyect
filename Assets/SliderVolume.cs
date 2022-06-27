using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    private void Update()
    {
        BackgroundMusic.UpdateValue(GetComponent<Slider>().value);
    }
}
