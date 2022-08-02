using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharStatus : MonoBehaviour
{
    [Header("HP Vars")]
    public float hp = 0;
    public float maxHp = 0;
    [SerializeField] private float hpPercent;
    [SerializeField] public Image _hpBar;

    [Header("Level Up Vars")]
    [SerializeField] private int _currentLvl = 0;
    [SerializeField] private float _currentExp = 0; //Exp actual
    [SerializeField] private float _expToLvlUp = 0; //Exp para lvlear
    [SerializeField] private float _expToLvlUpMultiplier = 0; //Multiplicador de exp para lvlear
    [SerializeField] private int _spPerLvlUp = 0; //SPs que me da lvlear
    [SerializeField] private Image _expBar;
    [SerializeField] private TextMeshProUGUI _lvlText;

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
    [SerializeField] private RespawnManager respawnManager;
    [SerializeField] private Boss boss;

    [Header("Take Damage Feedback")]
    public float flashTime;
    Color origionalColor;
    public Material renderer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            EventManager.Instance.Trigger("OnGettingHPG", 100f);
            EventManager.Instance.Trigger("OnGettingMPG", 100f);
            EventManager.Instance.Trigger("OnGettingRPG", 100f);
        }

    }

    private void Awake()
    {
        hp = maxHp;
        _anim = GetComponent<Animator>();
        _as = GetComponent<AbilitiesStatus>();

        //Seteo de vida
        hpPercent = hp / maxHp;
        _hpBar.fillAmount = hpPercent;

        //Seteo de Power gauges
        //Ranged
        _rangedPowerGauge = 0;
        _rpgPercent = _rangedPowerGauge / _maxPowerGauge;
        _rpgBar.fillAmount = _rpgPercent;

        //Melee
        _meleePowerGauge = 0;
        _mpgPercent = _meleePowerGauge / _maxPowerGauge;
        _mpgBar.fillAmount = _mpgPercent;

        //Ultimate
        _ultimatePowerGauge = 0;
        _upgPercent = _ultimatePowerGauge / _maxPowerGauge;
        _upgBar.fillAmount = _upgPercent;
    }

    void Start()
    {
        EventManager.Instance.Subscribe("OnIncreasingHp", IncreaseMaxHp); //Evento para generar mas vida
        EventManager.Instance.Subscribe("OnGettingExp", GetExp); //Evento para generar mas Nivel
        EventManager.Instance.Subscribe("OnGettingMPG", GetPowerGaugeMelee); //Evento para generar powergaguge ( melee )
        EventManager.Instance.Subscribe("OnGettingRPG", GetPowerGaugeRanged); //Evento para generar power gauge ( ranged )
        EventManager.Instance.Subscribe("OnGettingHPG", GetPowerGaugeHybrid); //Evento para generar power gauge ( hybrid )


        //renderer = GetComponentInChildren<Material>();

        origionalColor = renderer.color;


        _expBar.fillAmount = 0;


        _meleeSkillUp.SetActive(false);
        _rangedSkillUp.SetActive(false);
        _ultimateSkillUp.SetActive(false);
    }

    //Las gauges tnego que reworkearlos para que den para cada uno en especifica

    private void GetPowerGaugeRanged(params object[] parameters)
    {
        //Le doy mas puntos a la power gauge
        _rangedPowerGauge += (float)parameters[0];

        //Le actualizo el fill amount
        _rpgPercent = _rangedPowerGauge / _maxPowerGauge;
        _rpgBar.fillAmount = _rpgPercent;
        if (_rangedPowerGauge >= _maxPowerGauge - 1f)
        {
            _as.canCastAbility = true;

            _rangedPowerGauge = _maxPowerGauge;//Igualo

            _as.canUseRangedAbility = true;

            _rangedSkillUp.SetActive(true);
        }
    }

    private void GetPowerGaugeMelee(params object[] parameters)
    {
        //Le doy mas puntos a la power gauge
        _meleePowerGauge += (float)parameters[0];

        //Le actualizo el fill amount al melee
        _mpgPercent = _meleePowerGauge / _maxPowerGauge;
        _mpgBar.fillAmount = _mpgPercent;
        if (_meleePowerGauge >= _maxPowerGauge - 1f)
        {
            _as.canUseMeleeAbility = true;
            _meleePowerGauge = _maxPowerGauge;
            _as.canCastAbility = true;
            _meleeSkillUp.SetActive(true);
        }
    }

    private void GetPowerGaugeHybrid(params object[] parameters)
    {
        //Le doy mas puntos a la power gauge
        _ultimatePowerGauge += (float)parameters[0];

        //Le actualizo el fill amount al melee
        _upgPercent = _ultimatePowerGauge / _maxPowerGauge;
        _upgBar.fillAmount = _upgPercent;


        if (_ultimatePowerGauge >= _maxPowerGauge * .8f - 1f)
        {
            _as.canUseMixedAbility = true;
            _ultimatePowerGauge = _maxPowerGauge;
            _as.canCastAbility = true;
            _ultimateSkillUp.SetActive(true);
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
        _expBar.fillAmount = _currentExp / _expToLvlUp;
        if (_currentExp >= _expToLvlUp)// Si tengo suficiente experiencia, me lo lvlea
        {

            EventManager.Instance.Trigger("OnEarningSP", _spPerLvlUp); //Cuando Lvleo recibo Skill Points
            _currentExp = 0; //Resetea la exp de nivel
            _expBar.fillAmount = 0f;
            _currentLvl++; //Aumenta nivel
            AudioManager.PlaySound("LevelUp");
            _lvlText.text = _currentLvl.ToString();
            EventManager.Instance.Trigger("OnUpdatingLvl", _currentLvl);
            _expToLvlUp = _expToLvlUp * _expToLvlUpMultiplier; //Aumenta exp necesaria
        }
    }


    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        FlashRed();
        EZCameraShake.CameraShaker.Instance.ShakeOnce(1.2f, 1.2f, .1f, 1f);
        AudioManager.PlaySound("Hurt2");
        _hpBar.fillAmount = HpPercentCalculation(hp);
        if (hp <= 0)
        {
            boss.randomAttack = 3;
            respawnManager.RespawnCharacter();
        }


    }

    void FlashRed()
    {
        renderer.color = Color.red;
        Debug.Log(renderer.color);
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        renderer.color = Color.white;
        Debug.Log(renderer.color);
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
            _meleePowerGauge = 0;
            _mpgBar.fillAmount = 0;

            //Reduce el powergauge d ela ulti a la mitad
            _upgPercent = _ultimatePowerGauge * 0.5f / _maxPowerGauge;
            _ultimatePowerGauge *= .5f;
            _upgBar.fillAmount = _upgPercent;


            _meleeSkillUp.SetActive(false);
            _ultimateSkillUp.SetActive(false);
        }
        else if (abilityId == 1) //Ranged ability
        {
            _as.canUseRangedAbility = false;

            _rangedPowerGauge = 0;
            _rpgBar.fillAmount = 0;

            //Reduce el powergauge d ela ulti a la mitad
            _upgPercent = _ultimatePowerGauge * 0.5f / _maxPowerGauge;
            _ultimatePowerGauge *= .5f;
            _upgBar.fillAmount = _upgPercent;

            _ultimateSkillUp.SetActive(false);
            _rangedSkillUp.SetActive(false);

        }
        else if (abilityId == 2) //MixedAbility
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
