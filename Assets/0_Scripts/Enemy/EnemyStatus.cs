using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : EnemyData, IDamageable
{

    public GameObject onMeleeHittedParticles;
    public bool enemyHitted = false;
    private ButtonManager _bm;


    //Esta variable deberia estar idealmente en el enemydata pero NO TIME
    public bool isKeyOwner;
    public bool obtainedKey;

    //Este es el ultimo enemigo con llave
    public bool isLastKeyOwner;

    public void Start()
    {

        _bm = FindObjectOfType<ButtonManager>();

        _currentHp = _maxHp;
        _anim = GetComponent<Animator>();
        startPos = transform.position;

        obtainedKey = false;

        EventManager.Instance.Subscribe("OnGettingEnemyKey", GotKey);
        EventManager.Instance.Subscribe("OnGettingLastKeys", GotLastKey);
    }

    public void GotKey(params object [] parameters)
    {
        obtainedKey = true;
    }
    public void GotLastKey(params object[] parameters)
    {
        NewKeyPalancas.lastKeys++;
    }



    public void TakeDamage(int dmg)
    {
        //Le saco vida
        _currentHp -= dmg;


        if (_currentHp <= 0)
        {
            if (isWaveEnemy)
            {
                EventManager.Instance.Trigger("OnKillingWaveEnemy");
            }

            if (isKeyOwner)
            {
                EventManager.Instance.Trigger("OnGettingEnemyKey");
                AudioManager.PlaySound("LootKeySound");
            }

            if (isLastKeyOwner)
            {
                EventManager.Instance.Trigger("OnGettingLastKeys");
                AudioManager.PlaySound("LootKeySound");
            }

            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO MÁS
            if (TargetLock.enemiesClose.Contains(this.GetComponent<Enemy>()))
            {
                TargetLock.enemiesClose.Remove(this.GetComponent<Enemy>());
            }

            gameObject.SetActive(false);
        }
        //Estaria bueno hacer un pool de enemigos e irlos spawneando cada tanto
    }

    public void KnockBack()
    {
        _rb.AddForce(transform.forward * Time.deltaTime * -knockBackForce, ForceMode.Force);
    }


    private void OnTriggerEnter(Collider other)
    {
        //Cuando le pego con los ataques normales
        if (other.gameObject.layer == _hitboxLayermask && !other.CompareTag("Bullet"))
        {
            //Recibe dmg de lo que sea que valga el sword dmg, esto lo defino en combo asi siempre vale lo que yo quiero
            TakeDamage(Combo.swordDmg);
            Debug.Log("colisiona en el primero");
            //Instancia particulas
            //Ver de cambiar para que cada enemigo tenga su particula adentro y no la este INSTANCIANDO cada vez que le pego
            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);
            AudioManager.PlaySound("hit");
        }

        if (other.gameObject.layer == _abilityLayermask)
        {
            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            Debug.Log("le pegue aweonaou");

            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO MÁS
            if (TargetLock.enemiesClose.Contains(this.GetComponent<Enemy>()))
            {
                TargetLock.enemiesClose.Remove(this.GetComponent<Enemy>());
            }

            gameObject.SetActive(false);
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet") && !enemyHitted)
        {
            Debug.Log("estoy pegando anashe");
            TakeDamage(10);

            if(_bm.rangedUpgrade >= 1)
                EventManager.Instance.Trigger("OnGettingRPG", 25f/*Este valor es lo que me da de gauge*/);
            if (_bm.hybridUpgrade >= 1)
                EventManager.Instance.Trigger("OnGettingHPG", 6f/*Este valor es lo que me da de gauge*/);

            var instanstiatedParticles = Instantiate(onMeleeHittedParticles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.rotation);
            if (this.gameObject.activeSelf)
                StartCoroutine(WaitForEnemyHitted());
        }

        if (other.gameObject.layer == _abilityLayermask)
        {
            GetEXPPoints(_expPoints); //Los puntos de experiencia que se obtienen al matar al enemigo

            Debug.Log("le pegue aweonaou");

            //LO SACO DE LA LISTA PARA QUE UNA VEZ MUERTO NO PUEDA TARGETEARLO MÁS
            if (TargetLock.enemiesClose.Contains(this.GetComponent<Enemy>()))
            {
                TargetLock.enemiesClose.Remove(this.GetComponent<Enemy>());
            }

            gameObject.SetActive(false);
        }

    }



    IEnumerator WaitForEnemyHitted()
    {
        enemyHitted = true;
        yield return new WaitForSeconds(0.3f);
        enemyHitted = false;
    }
}
