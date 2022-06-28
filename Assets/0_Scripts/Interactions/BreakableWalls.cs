using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWalls : Objects, IDamageable
{
    public List<GameObject> walls;

    public List<Material> fadeWalls;

    public bool startScaling;
    public bool startFading;

    public float scaleValue;

    float timerScaling;

    public float timeScaling;

    public float fadeScale;

    float opacityValue;

    Vector3 vectorScale;

    private void Start()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            walls.Add(transform.GetChild(0).GetChild(i).gameObject);
        }

        opacityValue = 1;

        foreach (var wall in walls)
        {
            fadeWalls.Add(wall.GetComponent<MeshRenderer>().material);

        }

        vectorScale = new Vector3(scaleValue, scaleValue, scaleValue);

     

    }

    private void Update()
    {
        if (startScaling)
        {
            timerScaling += Time.deltaTime;

            foreach (var wall in walls)
            {
                transform.localScale -= vectorScale * Time.deltaTime;
            }
        }


        if (startFading)
        {
            opacityValue -= fadeScale * Time.deltaTime;

            foreach (var fadeWall in fadeWalls)
            {
                fadeWall.SetFloat("_Opacity", opacityValue);
            }
            
            if(opacityValue <= 0)
            {
                Destroy(gameObject);
            }
        }

        if(timerScaling >= timeScaling)
        {
            startScaling = false;
            startFading = true;
        }

    }

    public void TakeDamage(int dmg)
    {
        _maxHits -= dmg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _hitboxLayermask)
        {
            TakeDamage(_hitCost);

            GetComponent<Animator>().SetTrigger("OnHit");

            if(_maxHits <= 0)
            {
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
                startScaling = true;
            }
        }
    }
}
