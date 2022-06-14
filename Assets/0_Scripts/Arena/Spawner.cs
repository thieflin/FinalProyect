using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{ 

    public GameObject enemyToSpawn;

    public SpawnerManager instance;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = new Vector3(transform.localPosition.x + Random.Range(-5f, 5f), transform.position.y, transform.localPosition.z + Random.Range(-5f, 5f));
        var spawnedEnemy = Instantiate(enemyToSpawn, randomPosition, Quaternion.identity);
        
        instance.AddEnemy(spawnedEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
