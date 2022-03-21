using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform character;

    [SerializeField] public float offsetX;
    [SerializeField] public float offsetY;
    [SerializeField] public float offsetZ;



    void Update()
    {
        transform.position = new Vector3(character.transform.position.x - offsetX, character.transform.position.y - offsetY, character.transform.position.z - offsetZ);
    }
}
