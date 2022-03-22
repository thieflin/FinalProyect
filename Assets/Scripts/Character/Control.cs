using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{
    public PlayerMovement player;

    public float verticalMovement;
    public float horizontalMovement;


    public Control(PlayerMovement p)
    {
        player = p;
    }

    public void OnUpdate()
    {

        verticalMovement = Input.GetAxis("Vertical");
        horizontalMovement = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
            player.Dash();


        if (verticalMovement != 0 || horizontalMovement != 0)
            player.Move(direction);
        

    }
}
