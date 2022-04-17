using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Abilities : MonoBehaviour
{
    public bool isIdle;

    public CharStatus _cs;
    public Animator _anim;
    public Rigidbody rb;

    public List<GameObject> animColliders = new List<GameObject>();

    public int abilityId;

    public bool isPurchased;

    public virtual void OnUpdate()
    {

    }
    public virtual void Attack()
    {
        //Attack
    }
}
