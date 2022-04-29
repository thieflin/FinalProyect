using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public static int currentInstruction;
    public List<GameObject> instructions = new List<GameObject>();

    private void Update()
    {
        Debug.Log(currentInstruction);
    }
    private void OnTriggerEnter(Collider other)
    {
        var instTrigger = other.GetComponent<InstructionsTrigger>();
        

        if (instTrigger)
        {
            if (!instructions[currentInstruction].activeSelf)
            {
                instTrigger.triggerable = true;
                instructions[currentInstruction].SetActive(true);
                Pause.PauseGame();
            }
        }
    }
}
