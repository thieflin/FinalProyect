using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control
{
    public PlayerMovement player;

    public float verticalMovement;
    public float horizontalMovement;

    public float speedMultiplier = 200f;


    public Control(PlayerMovement p)
    {
        player = p;
    }

    public void OnUpdate()
    {

        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(horizontalMovement, 0, verticalMovement);

        if ((verticalMovement != 0 || horizontalMovement != 0) && player.isGrounded())
        {
            player.Move(direction);
        }
        else if ((verticalMovement != 0 || horizontalMovement != 0) && !player.isGrounded())
        {
            player.Move(new Vector3(direction.x * speedMultiplier, -10000f * Time.fixedDeltaTime, direction.z * speedMultiplier));
        }
        else if ((verticalMovement == 0 && horizontalMovement == 0) && !player.isGrounded())
        {
            player.Move(new Vector3(direction.x, -10000f * Time.fixedDeltaTime, direction.z));
        }
        else if (verticalMovement == 0 && horizontalMovement == 0 && player.isGrounded())
        {
            player.Move(new Vector3(0, -10000f * Time.fixedDeltaTime, 0));
        }

    }

   
}
