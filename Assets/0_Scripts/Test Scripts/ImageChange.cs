using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageChange : MonoBehaviour
{
    public GameObject deathFloor;
    public int returnalSpeed;
    public PlayerMovement pm;

    public static int zone;


    public List<GameObject> hiddenSpaces = new List<GameObject>();
    public List<GameObject> realSpaces = new List<GameObject>();

    private void Start()
    {
        zone = 0;
        foreach (var item in hiddenSpaces)
        {
            item.SetActive(false);
        }
    }
    

    public void AnimEventIn() 
    {
        deathFloor.SetActive(false);
        hiddenSpaces[zone].SetActive(true);
        realSpaces[zone].SetActive(false);
    }    
    public void AnimEventOut() 
    {
        deathFloor.SetActive(true);
        hiddenSpaces[zone].SetActive(false);
        realSpaces[zone].SetActive(true);
    }

    public void ReturnMovement()
    {
        pm._movementSpeed = returnalSpeed;
    }

}
