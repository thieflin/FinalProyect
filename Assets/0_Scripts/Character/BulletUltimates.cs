using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUltimates : MonoBehaviour
{
    public float speed;
    public float destroyTimer;

    private void Start()
    {
        destroyTimer = 6f;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        destroyTimer -= Time.deltaTime;
        if (destroyTimer < 0)
            Destroy(gameObject);
    }
}
