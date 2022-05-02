using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<GameObject> enemiesInWave = new List<GameObject>();


    public int waveScore;
    public int currentWave;


    public bool waveOn;


    private void Start()
    {
        waveOn = false;
        EventManager.Instance.Subscribe("OnKillingWaveEnemy", WaveScore);
    }

    public void WaveScore(params object[] parameters)
    {
        waveScore += 10;
    }

    //Spawnea a todos los enemigos
    public void SpawnEnemies()
    {
        //Activa todo los enemigos en la wave
        foreach (var enemy in enemiesInWave)
        {
            enemy.SetActive(true);
        }
    }

}
