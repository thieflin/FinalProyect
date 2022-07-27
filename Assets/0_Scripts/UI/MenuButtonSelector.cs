using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonSelector : MonoBehaviour
{

    public GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}