using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewKeyPalancas : MonoBehaviour
{
    //Estas son son sin llave
    public int normalAttackLayer;
    public bool isKeyMechanism;
    public bool gotKeyFromEnemy;
    public bool isLastElement;
    public Animator anim;

    public List<Animator> sticksToAnimate = new List<Animator>();

    private void Start()
    {
        GetComponent<Animator>();
    }

    public List<GameObject> blockColliders = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {
            //Si es de los que NO necesita llave
            if (!isKeyMechanism)
            {
                //Desactivo los colliders al pegarle
                foreach (var item in blockColliders)
                {
                    item.SetActive(false);
                }

                //Corro la animacion de apertura
                foreach (var item in sticksToAnimate)
                {
                    item.SetTrigger("Open");
                }
            }
            else if(isKeyMechanism)
            {
                if (gotKeyFromEnemy)
                {
                    
                    foreach (var item in blockColliders)
                    {
                        item.SetActive(false);
                    }

                    //Corro la animacion de apertura
                    foreach (var item in sticksToAnimate)
                    {
                        item.SetTrigger("Open");
                    }
                }
                else
                {
                    Debug.Log("No podes abrirloooo");
                }
            }
            else if (isLastElement)
            {
                //Aca es para el ultimo
            }
        }
    }

}
