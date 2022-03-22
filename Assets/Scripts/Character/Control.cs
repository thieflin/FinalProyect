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

        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);


        if (verticalMovement != 0 || horizontalMovement != 0)
            player.Move(direction);
        else
            player.Move(Vector3.zero);

        if (Input.GetKeyDown(KeyCode.Space))
            player.Dash();
    }
}
