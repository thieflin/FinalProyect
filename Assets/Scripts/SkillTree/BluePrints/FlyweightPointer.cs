using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightPointer : MonoBehaviour
{
    public static readonly Flyweight bluePrintElevator = new Flyweight
    {
        blueprintNumber = 0,
    }; 
    public static readonly Flyweight bluePrintKey = new Flyweight
    {
        blueprintNumber = 1,
    };

    public static readonly Flyweight playerLayerMask = new Flyweight
    {
        playerLayerMask = 7,
    };

    public static readonly Flyweight blueprintBoolean = new Flyweight
    {
        bluePrintBoolean = true,
    };

}
