using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatus : MonoBehaviour
{
    [Header("HP Vars")]
    public int hp = 0;
    public int maxHp = 0;

    [Header("Level Up Vars")]
    private int _currentLvl = 0;
    [SerializeField] private float _currentExp = 0; //Exp actual
    [SerializeField] private float _expToLvlUp = 0; //Exp para lvlear
    [SerializeField] private float _expToLvlUpMultiplier = 0; //Multiplicador de exp para lvlear
    [SerializeField] private float _spPerLvlUp = 0; //SPs que me da lvlear

    [Header("Power Gauge")]
    public float powerGauge;
    [SerializeField] private float _maxPowerGauge;
    [SerializeField] private float _skillMultiplier;
    [SerializeField] private bool _isFull;

    [SerializeField] private Animator _anim;


    private void Awake()
    {
        maxHp = hp = 100;
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        EventManager.Instance.Subscribe("OnIncreasingHp", IncreaseMaxHp); //Evento para generar mas vida
        EventManager.Instance.Subscribe("OnGettingExp", GetExp); //Evento para generar mas Nivel
        EventManager.Instance.Subscribe("OnActivatingSkill", SkillActivation); //Evento para activar la habilidad deseada
    }

    private void Update()
    {
        if(powerGauge < _maxPowerGauge)
        {
            powerGauge += Time.deltaTime*8;
            EventManager.Instance.Trigger("OnUpdatingPG", powerGauge);
            _isFull = false;
            if (powerGauge < 0)
                powerGauge = 0;
        }

        if(powerGauge >= _maxPowerGauge*_skillMultiplier)
        {
            EventManager.Instance.Trigger("OnUpdatingPG", powerGauge);

            _isFull = true;
        }
        if(powerGauge > _maxPowerGauge)
        {
            _isFull = true;
            EventManager.Instance.Trigger("OnUpdatingPG", powerGauge);

            powerGauge = _maxPowerGauge;
        }



    }

    //Esta funcion se llama junto con el que gasta skill points
    private void IncreaseMaxHp(params object[] parameters) //Aumenta la vida maxima
    {
        maxHp += (int)parameters[0];
        hp = maxHp;
        Debug.Log("Current HP: " + hp + ". Current MaxHP: " + maxHp + ".");
    }

    private void GetExp(params object[] parameters) //Sirve para cualquier elemento que le de experiencia
    {
        _currentExp += (float)parameters[0]; //Le paso la experiencia que me dan

        if(_currentExp >= _expToLvlUp)// Si tengo suficiente experiencia, me lo lvlea
        {
            EventManager.Instance.Trigger("OnEarningSP", _spPerLvlUp); //Cuando Lvleo recibo Skill Points
            _currentExp = 0; //Resetea la exp de nivel
            _currentLvl++; //Aumenta nivel
            EventManager.Instance.Trigger("OnUpdatingLvl", _currentLvl);
            _expToLvlUp = _expToLvlUp * _expToLvlUpMultiplier; //Aumenta exp necesaria
        }
    }

    private void SkillActivation(params object [] parameters) //Me setea TRUE el parametro que yo necesite del skill que quiera activar
    {
        _anim.SetBool((string)parameters[0], true);
    }

    public bool GetPowerGaugeBarStatus()
    {
        return _isFull;
    }

    public int GetCurrentLvl()
    {
        return _currentLvl;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
    }
}
