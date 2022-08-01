using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartScreenMenu : MonoBehaviour
{
    public GameObject startButton;
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
