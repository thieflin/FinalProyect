using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator elevatorAnim;
    [SerializeField] private ElevatorDoorBP _edbp;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == FlyweightPointer.playerLayerMask.playerLayerMask)
        {
            Debug.Log("lo toy tocando po");
            if (Input.GetKeyDown(KeyCode.E))
                if (_edbp.canActivate == true)
                    elevatorAnim.SetBool("Active", true);
                else Debug.Log("You dont have the blueprint");
            
        }

    }
}
