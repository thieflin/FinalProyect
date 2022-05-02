using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{

    [SerializeField] private PuertaPalanca assignedDoor;
    [SerializeField] private int normalAttackLayer;
    [SerializeField] private Animator _anim;
    [SerializeField] private Animator _animDoor;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _animDoor = assignedDoor.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == normalAttackLayer)
        {
            //Desactiva uno de los locks de la puerta
            assignedDoor.locks[assignedDoor.currentLevers].SetActive(false);
            //Sube la cantidad
            assignedDoor.currentLevers++;
            //Self trigger de animacion
            _anim.SetTrigger("LeverHit");
            //Le desactivo el collider para que no le pegue mas
            gameObject.GetComponent<BoxCollider>().enabled = false;

            //Si ya estan todas las palancas
            if (assignedDoor.requiredLevers == assignedDoor.currentLevers)
            {
                AudioManager.PlaySound("puzzle");
                Destroy(assignedDoor.gameObject);
                //_animDoor.SetTrigger("OpenDoor"); por el momento simplemente se va a destruir
            }
                
                
        }
    }
}
