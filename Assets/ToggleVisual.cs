using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleVisual : MonoBehaviour
{
    Toggle myToggle;
    public GameObject togleOff;

    private void Awake()
    {
        myToggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myToggle.isOn)
        {
            togleOff.SetActive(false);
        }
        else
        {
            togleOff.gameObject.SetActive(true);
        }
    }
}
