using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUltimates : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
