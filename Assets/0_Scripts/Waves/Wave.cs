using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> enemiesInWave1 = new List<GameObject>();
    public List<GameObject> enemiesInWave2 = new List<GameObject>();
    public GameObject lockExitDoor;
    public GameObject winExitDoor;
    public bool canSpawnWave2;

    public int waveScore;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (other)
        {
            winExitDoor.GetComponent<Animator>().SetTrigger("StartWave");
            lockExitDoor.GetComponent<Animator>().SetTrigger("StartWave");
            foreach (var item in enemiesInWave1)
            {
                item.SetActive(true);
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    void SpawnOtherWaveParts()
    {
        foreach (var item in enemiesInWave2)
        {
            item.SetActive(true);
        }
    }

    private void Start()
    {
        EventManager.Instance.Subscribe("OnKillingWaveEnemy", WaveScore);
        canSpawnWave2 = true;
    }

    public void WaveScore(params object[] parameters)
    {
        waveScore += 10;
        if (waveScore >= 50 && canSpawnWave2)
        {
            SpawnOtherWaveParts();
            canSpawnWave2 = false;
        }
        else if (waveScore >= 100)
        {
            winExitDoor.GetComponent<Animator>().SetTrigger("BeatWave");
        }
    }

    private void Update()
    {

    }
}
