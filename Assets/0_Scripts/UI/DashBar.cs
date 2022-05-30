using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider dashSlider;

    public static DashBar instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    
}
