using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public GameObject decorationElements;
    public GameObject insideElements;
    public int returnalSpeed;
    public PlayerMovement pm;

    private void Start()
    {
        insideElements.SetActive(false);
    }
    

    public void AnimEventIn() 
    {
        decorationElements.SetActive(false);
        insideElements.SetActive(true);
    }    
    public void AnimEventOut() 
    {
        decorationElements.SetActive(true);
        insideElements.SetActive(false);
    }

    public void ReturnMovement()
    {
        pm._movementSpeed = returnalSpeed;
    }

}
