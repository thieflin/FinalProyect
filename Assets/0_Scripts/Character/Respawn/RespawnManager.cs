using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public List<GameObject> respawnList = new List<GameObject>();

    public GameObject player;

    public Animator fadeAnimation;

    public static Transform playerRespawn;

    private void Start()
    {

    }

    public void RespawnCharacter()
    {
        StartCoroutine(RespawnAnimation());
    }

    IEnumerator RespawnAnimation()
    {
        fadeAnimation.SetBool("FadeIn", true);

        yield return new WaitForSeconds(2f);

        fadeAnimation.SetBool("FadeIn", false);

        player.transform.position = playerRespawn.transform.position;
        var charStatus = player.GetComponent<CharStatus>();
        charStatus.hp = charStatus.maxHp;
        charStatus._hpBar.fillAmount = charStatus.HpPercentCalculation(charStatus.hp);

        //player.GetComponent<PlayerMovement>().isTargeting = false;

        fadeAnimation.SetBool("FadeOut",true);

        yield return new WaitForSeconds(0.75f);

        fadeAnimation.SetBool("FadeOut", false);
        
    }
}
