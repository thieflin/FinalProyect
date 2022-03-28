using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSPSpendSP : MonoBehaviour
{
    public int spGain;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            EventManager.Instance.Trigger("OnEarningSP", spGain);
        }
    }
}
