using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorBP : Blueprint
{
    private void Start()
    {
        canActivate = false;
    }
    public override void ActivateBlueprintType()
    {
        EventManager.Instance.Trigger("OnObtainingBlueprint", FlyweightPointer.bluePrintElevator.blueprintNumber);
        canActivate = true;
        //Obtiene SP
        Debug.Log("agarre la blueprint");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == FlyweightPointer.playerLayerMask.playerLayerMask)
            ActivateBlueprintType();
    }

}
