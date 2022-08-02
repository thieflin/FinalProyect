using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip lootingCollectible, finishingPuzzle, hitting, slashing, purchase, failtopurchase, bossGrab, robotMeleeHitting, robotHitten;
    public static AudioClip shotgunSound, healSound, lootKeySound, levelUpSound, hurtSound, dashSound, puzzleSolve;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        lootingCollectible = Resources.Load<AudioClip>("collect");
        finishingPuzzle = Resources.Load<AudioClip>("puzzle");
        hitting = Resources.Load<AudioClip>("hit");
        slashing = Resources.Load<AudioClip>("slash");
        purchase = Resources.Load<AudioClip>("BuyingAbility");
        failtopurchase = Resources.Load<AudioClip>("AbilityFailPurchase");
        bossGrab = Resources.Load<AudioClip>("RobotGrab");
        robotMeleeHitting = Resources.Load<AudioClip>("RobotMeleeHitting");
        robotHitten = Resources.Load<AudioClip>("RobotHitted");
        shotgunSound = Resources.Load<AudioClip>("ShotgunShort");
        healSound = Resources.Load<AudioClip>("HealShortA");
        lootKeySound = Resources.Load<AudioClip>("LootKeySound");
        levelUpSound = Resources.Load<AudioClip>("LevelUp");
        hurtSound = Resources.Load<AudioClip>("Hurt2");
        dashSound = Resources.Load<AudioClip>("DashCorto");
        puzzleSolve = Resources.Load<AudioClip>("PuzzleSolveCrop");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "collect":
                audioSrc.PlayOneShot(lootingCollectible);
                break;
            case "puzzle":
                audioSrc.PlayOneShot(finishingPuzzle);
                break;
            case "hit":
                audioSrc.PlayOneShot(hitting);
                break;
            case "slash":
                audioSrc.PlayOneShot(slashing);
                break;
            case "AbilityFailPurchase":
                audioSrc.PlayOneShot(failtopurchase);
                break;
            case "BuyingAbility":
                audioSrc.PlayOneShot(purchase);
                break;
            case "RobotGrab":
                audioSrc.PlayOneShot(bossGrab);
                break;
            case "RobotMeleeHitting":
                audioSrc.PlayOneShot(robotMeleeHitting);
                break;
            case "RobotHitted":
                audioSrc.PlayOneShot(robotHitten);
                break;
            case "ShotgunShort":
                audioSrc.PlayOneShot(shotgunSound);
                break;
            case "HealShortA":
                audioSrc.PlayOneShot(healSound);
                break;
            case "LootKeySound":
                audioSrc.PlayOneShot(lootKeySound);
                break;
            case "LevelUp":
                audioSrc.PlayOneShot(levelUpSound);
                break;
            case "Hurt2":
                audioSrc.PlayOneShot(hurtSound);
                break;
            case "DashCorto":
                audioSrc.PlayOneShot(dashSound);
                break;
            case "PuzzleSolveCrop":
                audioSrc.PlayOneShot(puzzleSolve);
                break;

            default:
                break;
        }
    }
}
