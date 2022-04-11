using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEXP : MonoBehaviour
{
    public float getExp;
    public void GetExpPoints()
    {
        EventManager.Instance.Trigger("OnGettingExp", getExp);
    }
}
