using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashForwardUlti : MonoBehaviour
{
    public float speed;
    public void OnEnable()
    {
        transform.position = transform.parent.position;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
