using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{

    private float _skillPoints = 0;
    [SerializeField] private GameObject _skillTree = null;
    [SerializeField] private List<GameObject> _skillsImage = null;
    public List<bool> _skillActivation = new List<bool>();

    private void Awake()
    {
        _skillTree.SetActive(false);
        ////JURO QUE LO VOY A ARREGLAR Y PONERLO BELLO PIDO DISCULPAS PUBLICAS POR EL MOMENTO
        //_skillActivation[0] = false;
        //_skillActivation[1] = false;
        //_skillActivation[2] = false;
        ////JURO QUE LO VOY A ARREGLAR Y PONERLO BELLO PIDO DISCULPAS PUBLICAS POR EL MOMENTO

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
            if (_skillTree.activeSelf)
                _skillTree.SetActive(false);
            else _skillTree.SetActive(true);
    }

    private void Start()
    {
        EventManager.Instance.Subscribe("OnEarningSP", EarningSp);
        EventManager.Instance.Subscribe("OnSpendingSP", SpendingSp);
        EventManager.Instance.Subscribe("OnSkillPurchase", SkillActivation);
    }

    private void EarningSp(params object[] parameters)
    {
        _skillPoints += (float)parameters[0];
        Debug.Log(_skillPoints);
    }

    private void SpendingSp(params object[] parameters)
    {
        _skillPoints -= (float)parameters[0];
        Debug.Log(_skillPoints);

    }

    private void SkillActivation(params object[] parameters) //Me activa el skill que yo compre
    {
        for (int i = 0; i < _skillActivation.Count; i++)
        {
            _skillActivation[(int)parameters[0]] = true;
            _skillsImage[(int)parameters[0]].SetActive(true);
        }
    }

    public float CurrentSkillPoints()
    {
        return _skillPoints;
    }
}
