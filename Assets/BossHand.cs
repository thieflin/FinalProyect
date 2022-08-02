using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    public float knockBackForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharStatus>())
        {
            var character = other.GetComponent<CharStatus>();

            character.TakeDamage(15);

            character.GetComponent<Rigidbody>().AddForce(character.transform.forward * -1 * knockBackForce * Time.deltaTime, ForceMode.Impulse);

            Debug.Log("ENTRE");
        }
    }
}
