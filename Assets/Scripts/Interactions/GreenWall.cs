using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWall : MonoBehaviour
{
    [SerializeField] public GameObject door;
    [SerializeField] public GameObject player;
    public bool openWall = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (openWall)
                Destroy(door);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            openWall = true;
    }


}
