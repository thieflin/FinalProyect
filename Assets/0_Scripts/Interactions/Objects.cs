using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objects : MonoBehaviour
{
    [SerializeField] protected int _hitCost;
    [SerializeField] protected int _maxHits;
    [SerializeField] protected int _hitboxLayermask;


}
