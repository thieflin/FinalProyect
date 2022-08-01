using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorPosition : MonoBehaviour
{
    public GameObject selector;
    public List<GameObject> selectors = new List<GameObject>();

    public void ChangePosition()
    {
        selector.SetActive(true);
        foreach (var item in selectors)
        {
            item.SetActive(false);
        }
        selector.transform.position = new Vector3(selector.transform.position.x, transform.position.y, transform.position.z);
    }

    public void ShowSelector(int id)
    {
        selectors[id].SetActive(true);
    }

    public void HideSelector(int id)
    {
        selectors[id].SetActive(false);

    }

    public void HideSelectorMouse()
    {
        selector.SetActive(false);
    }
}
