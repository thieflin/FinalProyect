using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{


    StateMachine _fms;
    Hunter _hunter;
    bool isResting;

    public IdleState(StateMachine fms, Hunter h)
    {
        _fms = fms;
        _hunter = h;
    }



    public void OnExit()
    {
        _hunter.anim.SetBool("IdleB", false);
    }

    public void OnStart()
    {


        _hunter.anim.SetBool("IdleB", true);
        _hunter.thinkingmeter = 3f;

        //if (_hunter.staminaBar <= 0) //Si la stamina es menor que 0 le activo para que descanse
        //    isResting = true;
        //else //Si no, en el start directamente me voy al patrol 
        //    _fms.ChangeState(PlayerStatesEnum.Patrol);


    }

    public void OnUpdate()
    {
        //Busco el vector al cual yo quiero rotar es decir donde llegue ahi freno
        Quaternion toRotation = Quaternion.LookRotation(-_hunter.transform.position + _hunter.allWaypoints[_hunter.currentWaypoint].transform.position);

        //Hago que la rotacion sea un rotate towards hacia el vector calculado antes
        _hunter.transform.rotation = Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime);

        //Cuando llega ahi deja de rotar
        if(_hunter.transform.rotation == Quaternion.RotateTowards(_hunter.transform.rotation, toRotation, _hunter.rotationSpeedOnIdle * Time.deltaTime))
            _fms.ChangeState(PlayerStatesEnum.Patrol);


        //Thinking time 
        //_hunter.thinkingmeter -= Time.deltaTime;
        //if (_hunter.thinkingmeter < 0) _fms.ChangeState(PlayerStatesEnum.Patrol);


        //if (isResting && _hunter.staminaBar <=10) //una vez que me quedo sin stamina vengo aca, si se hizo true resting en el start
        //{
        //    _hunter.waypointSpeed = 0; //Le hago 0 la speed de movimiento
        //    _hunter.staminaBar += 4*Time.deltaTime; //Lo pongo a recargar energia

        //}
        //else //Si no estoy en resting, lo paso como false, le doy su speed, y me vuelvo a patrol
        //{
        //    //_hunter.waypointSpeed = 4;
        //    isResting = false;
        //    _fms.ChangeState(PlayerStatesEnum.Patrol);
        //}
    }
}
