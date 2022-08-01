using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMelee : MonoBehaviour, IState
{


    StateMachine _fms;
    HunterMelee _hunter;


    public AttackStateMelee(StateMachine fms, HunterMelee h)
    {
        _fms = fms;
        _hunter = h;
    }

    private float meassure;

    public void OnExit()
    {
        //Desactivo las particulas cuando salgo del attack state
        _hunter.justAttacked = false;
    }

    public void OnStart()
    {
        _hunter.anim.SetTrigger("Attack");
        //meassure = 3f;
    }

    //Falta hacer que cuando cargue te mire
    public void OnUpdate()
    {
        Debug.Log("en attack el ranged");
        AttackAnimation();

        //FocusPlayer();

        //comenta3 x s ilas dudas este codigo si algo no anda borralo y proba de vuelta XD

        //meassure -= Time.deltaTime;
        //if (meassure < 0) _fms.ChangeState(PlayerStatesEnum.Idle);
    }

    public void FocusPlayer()
    {
        //Esto hace que lo mire al perseguirlo
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.target.transform.position);
        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);
    }

    public void AttackAnimation()
    {
        _hunter.rb.AddForce(_hunter.transform.forward * _hunter.attackForces * Time.deltaTime, ForceMode.Impulse);
    }
}
