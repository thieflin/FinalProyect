using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(DestroyParticlesTime());
    }

    IEnumerator DestroyParticlesTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }


}
