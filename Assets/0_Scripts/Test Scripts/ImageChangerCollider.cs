using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChangerCollider : MonoBehaviour
{
    //Esto requiere SIEMPRE un mini collider para que de el tiempo a prender las cosas y el jugador no se caiga.
    public int playerLayerMask;
    public Animator anim;
    public PlayerMovement pm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayerMask) //Cuando entro a la zona apago todo y triggereo la anim
        {
            anim.SetTrigger("IsInside");
            pm._movementSpeed = 0;
        }
    }

    private void OnTriggerExit(Collider other) //CCuando salgo de la zona aviso y tambien paro la movespeed
    {
        if (other.gameObject.layer == playerLayerMask)
        {
            anim.SetTrigger("IsOutside");
            pm._movementSpeed = 0;

        }
    }
}
