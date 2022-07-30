using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorPosition : MonoBehaviour
{
    public GameObject selector;

    public void ChangePosition()
    {
        selector.transform.position = new Vector3(selector.transform.position.x, transform.position.y, transform.position.z);
    }
}
