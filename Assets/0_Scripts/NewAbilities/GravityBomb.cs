using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityBomb : MonoBehaviour
{
    public float effectiveRadius;
    public float pullForce;
    public LayerMask enemiesLayermask, wallsMask;
    public List<TestGravity> enemiesInRange = new List<TestGravity>();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, effectiveRadius);
    }

    private void Start()
    {
        EnemiesToPull();
        foreach (var item in enemiesInRange)
        {
            Debug.Log("a este lo agregue");
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.R))
        {
            
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PullActivation();
        }
    }

    public void ForceCalculator()
    {
        
    }


    void EnemiesToPull() //Reviso que enemigos son targeteables
    {
        var pullSphere = Physics.OverlapSphere(transform.position, effectiveRadius, enemiesLayermask); //Una esfera de colisiones
        foreach (var item in pullSphere)
        {
            //Miro la posicion de los enemigos y la gravity sphere, tiro un raycast a cada uno.
            Vector3 dir = item.transform.position - transform.position;
            
            if (!Physics.Raycast(transform.position, dir, dir.magnitude, wallsMask))
            {
                var enemy = item.GetComponent<TestGravity>();
                enemiesInRange.Add(enemy);
            }
        }
    }

    void PullActivation()
    {
        foreach (var item in enemiesInRange)
        {
            Vector3 dir = transform.position - item.transform.position;
            //Esto lo que hace es en base a la fuerza maxima, y el radio de efectividad, chequea la distancia
            //y aplica fuerza dependiendo de la distancia
            float forceModifier = Mathf.Lerp(effectiveRadius, pullForce, dir.magnitude / effectiveRadius);
            Debug.Log(forceModifier);
            var force = item.GetComponent<Rigidbody>();
            force.AddForce(dir * forceModifier * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
