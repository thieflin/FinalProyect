using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;

    public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
        }
    }


    public void AddEnemy(GameObject enemy)
    {
        if (!enemies.Contains(enemy))
            enemies.Add(enemy);
        else
            return;
    }

    public void RemoveEnemy()
    {
        if (enemies.Count > 0)
        {
            Destroy(enemies[enemies.Count - 1]);
            enemies.RemoveAt(enemies.Count - 1);
        }
    }

}
