using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blueprint : MonoBehaviour
{
    public bool canActivate;
    public virtual void ActivateBlueprintType()
    {
        //Activa blueprints
    }
}
