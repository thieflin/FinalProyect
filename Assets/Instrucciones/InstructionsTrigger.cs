using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTrigger : MonoBehaviour
{
    [SerializeField] private int thisInstruction;
    public GameObject instruction;
    public bool triggerable;

    private void Start()
    {
        triggerable = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (triggerable)
            {
                Pause.UnpauseGame();
                Instructions.currentInstruction++;
                instruction.SetActive(false);
                gameObject.SetActive(false);
            }

        }
    }
}
