using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCutoutWall : MonoBehaviour
{
    [SerializeField] private Material wallMaterial = null;
    [SerializeField] private Camera Camera = null;
    [SerializeField] private LayerMask wallMask = 0;
    [Range(0f, 500f)]
    [SerializeField] private int maxCutout = 0;
    [Range(0f, 1f)]
    [SerializeField] private float opacity= 0;
    [SerializeField] private float speedOpacity = 0;

    public void Start()
    {
        var view = Camera.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector("_PlayerPosition", -view);
        wallMaterial.SetFloat("_CutoutSizee", 0);
    }

    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir);

        if (Physics.Raycast(ray, maxCutout, wallMask) && opacity < 1)
        {
            opacity += Time.deltaTime * speedOpacity;
            wallMaterial.SetFloat("_CutoutSizee", opacity);
        }
        else if(!Physics.Raycast(ray, maxCutout, wallMask) && opacity > 0)
        {
            opacity -= Time.deltaTime * speedOpacity;
            wallMaterial.SetFloat("_CutoutSizee", opacity);
        }

        var view = Camera.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector("_PlayerPosition", -view);
    }
}
