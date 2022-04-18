using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMovement : MonoBehaviour
{
    private int speed;
    public Transform savedPos;
    private void Start()
    {
        speed = 40;
    }
    public void OnDisable()
    {
        transform.position = savedPos.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
