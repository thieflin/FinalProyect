using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrucLootables : MonoBehaviour
{
    public bool looted;
    [SerializeField] private GameObject instruction;
    // Update is called once per frame
    private void Start()
    {
        looted = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (looted)
            {
                Pause.UnpauseGame();
                instruction.SetActive(false);
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();

        if (player)
        {
            looted = true;
            instruction.SetActive(true);
            Pause.PauseGame();
        }
    }
}
