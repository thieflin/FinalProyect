using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<GameObject> respawnList = new List<GameObject>();

    public GameObject player;

    public Animator fadeAnimation;

    public static int playerRespawnNumber;

    private void Start()
    {
        playerRespawnNumber = 0;
    }

    public void RespawnCharacter()
    {
        StartCoroutine(RespawnAnimation());
    }

    IEnumerator RespawnAnimation()
    {
        fadeAnimation.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2f);

        //ACCEDO A LA LISTA DE ENEMIGOS QUE TENGA EL RESPAWN
        foreach (var respawn in respawnList[playerRespawnNumber].GetComponent<RespawnTrigger>().enemiesToRespawn)
        {
            //VUELVO A ACTIVAR LOS ENEMIGOS MUERTOS
            if (!respawn.gameObject.activeSelf)
            {
                respawn.gameObject.SetActive(true);
            }

            var enemy = respawn.GetComponent<EnemyStatus>();

            //PARA QUE NO LO SIGAN APENAS RESPAWNEA
            enemy.GetComponent<Enemy>().player = null;
            enemy.GetComponent<Enemy>().playerIsInSight = false;
            enemy._currentHp = enemy.GetMaxHP();
            enemy.transform.position = enemy.startPos;

        
        }

        player.transform.position = respawnList[playerRespawnNumber].transform.position;
        var charStatus = player.GetComponent<CharStatus>();
        charStatus.hp = charStatus.maxHp;
        charStatus._hpBar.fillAmount = charStatus.HpPercentCalculation(charStatus.hp);
        player.GetComponent<PlayerMovement>().isTargeting = false;
        fadeAnimation.SetTrigger("FadeOut");
        
    }
}
