using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITextTransfer : MonoBehaviour
{
    public TextMeshProUGUI spText;
    public TextMeshProUGUI lvlText;
    public TextMeshProUGUI pGText;
    void Start()
    {
        EventManager.Instance.Subscribe("OnUpdatingSp", ChangeSpText);
        EventManager.Instance.Subscribe("OnUpdatingLvl", ChangeLvlText);
        EventManager.Instance.Subscribe("OnUpdatingPG", ChangePGText);
    }

    public void ChangeSpText(params object[] parameters)
    {
        var num = (float)parameters[0];
        spText.text = num.ToString();
    }

    public void ChangeLvlText(params object[] parameters)
    {
        var num = (int)parameters[0];
        lvlText.text = num.ToString();
    }

    public void ChangePGText(params object[] parameters)
    {
        var num = (float)parameters[0];
        pGText.text = num.ToString();
    }
}
