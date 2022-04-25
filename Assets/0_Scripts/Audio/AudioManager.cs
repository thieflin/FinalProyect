using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip lootingCollectible, finishingPuzzle, hitting;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        lootingCollectible = Resources.Load<AudioClip>("collect");
        finishingPuzzle = Resources.Load<AudioClip>("puzzle");
        hitting = Resources.Load<AudioClip>("hit");

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
            

            default:
                break;
        }
    }
}
