using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStatus : MonoBehaviour
{
    [Header("HP Vars")]
    public float hp = 0;
    public float maxHp = 0;
    [SerializeField] private float hpPercent;
    [SerializeField] private Image _hpBar;

    [Header("Level Up Vars")]
    private int _currentLvl = 0;
    [SerializeField] private float _currentExp = 0; //Exp actual
    [SerializeField] private float _expToLvlUp = 0; //Exp para lvlear
    [SerializeField] private float _expToLvlUpMultiplier = 0; //Multiplicador de exp para lvlear
    [SerializeField] private int _spPerLvlUp = 0; //SPs que me da lvlear

    [Header("Power Gauge")]
    public float powerGauge;
    [SerializeField] private float _maxPowerGauge;

    [SerializeField] private Image _mpgBar;
    [SerializeField] private float _mpgPercent;

    [SerializeField] private Image _rpgBar;
    [SerializeField] private float _rpgPercent;

    [SerializeField] private Image _upgBar;
    [SerializeField] private float _upgPercent;


    [SerializeField] private float _meleePowerGauge;
    [SerializeField] private float _rangedPowerGauge;
    [SerializeField] private float _ultimatePowerGauge;

    [Header("Skills borders")]
    [SerializeField] private GameObject _meleeSkillUp;
    [SerializeField] private GameObject _rangedSkillUp;
    [SerializeField] private GameObject _ultimateSkillUp;


    [Header("Referencias")]
    [SerializeField] private Animator _anim;
    [SerializeField] private AbilitiesStatus _as;


    private void Awake()
    {
        maxHp = hp = 100;
        _anim = GetComponent<Animator>();
        _as = GetComponent<AbilitiesStatus>();

        //Seteo de vida
        hpPercent = hp / maxHp;
        _hpBar.fillAmount = hpPercent;

        //Seteo de Power gauges
        //Ranged
        _rangedPowerGauge = 50;
        _rpgPercent = _rangedPowerGauge / _maxPowerGauge;
        _rpgBar.fillAmount = _rpgPercent;

        //Melee
        _meleePowerGauge = 50;
        _mpgPercent = _meleePowerGauge / _maxPowerGauge;
        _mpgBar.fillAmount = _mpgPercent;

        //Ultimate
        //_upgPercent = _ultimatePowerGauge / _maxPowerGauge * 2f;
        //_upgBar.fillAmount = _upgPercent;
    }

    void Start()
    {
        EventManager.Instance.Subscribe("OnIncreasingHp", IncreaseMaxHp); //Evento para generar mas vida
        EventManager.Instance.Subscribe("OnGettingExp", GetExp); //Evento para generar mas Nivel
        EventManager.Instance.Subscribe("OnGettingMPG", GetPowerGaugeMelee); //Evento para generar powergaguge ( melee )
        EventManager.Instance.Subscribe("OnGettingRPG", GetPowerGaugeRanged); //Evento para generar power gauge ( ranged )
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventManager.Instance.Trigger("OnGettingMPG", 10f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventManager.Instance.Trigger("OnGettingRPG", 10f);
        }
    }

    private void GetPowerGaugeRanged(params object[] parameters)
    {
        if (_rangedPowerGauge >= _maxPowerGauge)
        {
            _as.canCastAbility = true;
            _rangedPowerGauge = _maxPowerGauge;
            _as.canUseRangedAbility = true;
            _rangedSkillUp.SetActive(true);
            //Le pregunto si esta maxeado
            if (_ultimatePowerGauge >= _maxPowerGauge * 2)
            {
                _ultimateSkillUp.SetActive(true);
                _as.canUseMixedAbility = true;
            }
        }
        else
        {

            //Le doy mas puntos a la power gauge
            _rangedPowerGauge += (float)parameters[0];

            //Le actualizo el fill amount
            _rpgPercent = _rangedPowerGauge / _maxPowerGauge;
            _rpgBar.fillAmount = _rpgPercent;

            //Le doy puntos de la power gauge de la ulti
            _ultimatePowerGauge += (float)parameters[0];

            //Le actualizo el fill amount a la ulti
            //_upgPercent = _ultimatePowerGauge / _maxPowerGauge * 2;
            //_upgBar.fillAmount = _upgPercent;

            //Le pregunto si esta maxeado
            if(_ultimatePowerGauge >= _maxPowerGauge * 2)
            {
                _as.canUseMixedAbility = true;
            }
        }
    }

    private void GetPowerGaugeMelee(params object[] parameters)
    {

        if (_meleePowerGauge >= _maxPowerGauge)
        {
            _as.canUseMeleeAbility = true;
            _meleePowerGauge = _maxPowerGauge;
            _as.canCastAbility = true;
            _rangedSkillUp.SetActive(true);
            //Le pregunto si esta maxeado
            if (_ultimatePowerGauge >= _maxPowerGauge * 2)
            {
                _ultimateSkillUp.SetActive(true);
                _as.canUseMixedAbility = true;
            }
        }
        else
        {
            //Le doy mas puntos a la power gauge
            _meleePowerGauge += (float)parameters[0];

            //Le actualizo el fill amount al melee
            _mpgPercent = _meleePowerGauge / _maxPowerGauge;
            _mpgBar.fillAmount = _mpgPercent;


            //Le doy puntos de la power gauge de la ulti
            _ultimatePowerGauge += (float)parameters[0];


            //Le actualizo el fill amount a la ulti
            //_upgPercent = _ultimatePowerGauge / _maxPowerGauge * 2;
            //_upgBar.fillAmount = _upgPercent;


            //Le pregunto si esta maxeado
            if (_ultimatePowerGauge >= _maxPowerGauge * 2)
            {
                _as.canUseMixedAbility = true;
            }
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

        if (_currentExp >= _expToLvlUp)// Si tengo suficiente experiencia, me lo lvlea
        {
            EventManager.Instance.Trigger("OnEarningSP", _spPerLvlUp); //Cuando Lvleo recibo Skill Points
            _currentExp = 0; //Resetea la exp de nivel
            _currentLvl++; //Aumenta nivel
            EventManager.Instance.Trigger("OnUpdatingLvl", _currentLvl);
            _expToLvlUp = _expToLvlUp * _expToLvlUpMultiplier; //Aumenta exp necesaria
        }
    }


    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        _hpBar.fillAmount = HpPercentCalculation(hp);
    }

    public float HpPercentCalculation(float actualHp)
    {
        hpPercent = actualHp / maxHp;
        return hpPercent;
    }


    public void UseAbility(int abilityId)
    {
        if (abilityId == 0) //Melee ability
        {
            _as.canUseMeleeAbility = false;
            _as.canUseMixedAbility = false;
            _meleePowerGauge = 0;
            _mpgBar.fillAmount = 0;
            _meleeSkillUp.SetActive(false);
            _ultimateSkillUp.SetActive(false);
        }
        else if (abilityId == 1) //Ranged ability
        {
            _as.canUseRangedAbility = false;
            _as.canUseMixedAbility = false;
            _rangedPowerGauge = 0;
            _rpgBar.fillAmount = 0;
            _ultimateSkillUp.SetActive(false);
            _rangedSkillUp.SetActive(false);

        }
        else //MixedAbility
        {
            _as.canUseMeleeAbility = false;
            _as.canUseRangedAbility = false;
            _as.canUseMixedAbility = false;
            _meleePowerGauge = 0;
            _rangedPowerGauge = 0;
            _ultimatePowerGauge = 0;
            _rpgBar.fillAmount = 0;
            _mpgBar.fillAmount = 0;
            _upgBar.fillAmount = 0;
            _ultimateSkillUp.SetActive(false);
            _meleeSkillUp.SetActive(false);
            _rangedSkillUp.SetActive(false);

        }

    }



}
