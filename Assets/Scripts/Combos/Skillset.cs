using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillset : MonoBehaviour
{

    [SerializeField] protected int _abilityCd;
    protected SkillTree _st;
    public virtual void Attack()
    {
        //Attack pattern or w.e when i need it
    }
}
