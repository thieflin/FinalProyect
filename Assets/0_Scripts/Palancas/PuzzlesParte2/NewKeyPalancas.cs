using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewKeyPalancas : MonoBehaviour
{
    
    public int normalAttackLayer;

    //Si necesitan llave
    public bool isKeyMechanism;
    public bool gotKeyFromEnemy;
    
    //Si necesitan llave y son el ultimo elemento
    public bool isLastElement;
    public static int lastKeys;
    public static int activatedDevices;

    public Animator anim;

    //Enemigos que tienen llave
    public List<EnemyStatus> enemiesWithKey = new List<EnemyStatus>();

    //Palancas que hay que animar
    public List<Animator> sticksToAnimate = new List<Animator>();

    private void Start()
    {
        GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.Log(lastKeys);
    }
    public List<GameObject> blockColliders = new List<GameObject>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == normalAttackLayer)
        {
            //Si es de los que NO necesita llave
            if (!isKeyMechanism && !isLastElement)
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
            //Si es mecanismo de llave
            else if (isKeyMechanism)
            {

                //Si tengo la llave ejecuto lo de siempre
                if (enemiesWithKey[0].obtainedKey == true)
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
            else if (isLastElement && !isKeyMechanism)
            {
                //Agarro una llave y le pego 
                if(lastKeys > 0)
                {
                    //Activo los devices
                    activatedDevices++;
                    gameObject.GetComponent<BoxCollider>().enabled = false;


                    //Entonces si tengo 2 le pego dos veces y queda en 0

                    if (activatedDevices >= 2)
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

                    //En teoria si tengo una sola lalve puedo pegarle una vez
                    lastKeys--;
                }
                else
                {
                    Debug.Log("xd");
                }
               
            }
        }
    }

}
