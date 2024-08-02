using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem water;
    [SerializeField] private double WaterCapacity = 5;
    [SerializeField] private GameObject Bottleneck;
    [SerializeField] private GameObject Bottom;
    // Start is called before the first frame update
    void Start()
    {
        water = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Bottleneck.transform.position.y <= Bottom.transform.position.y)
            water.enableEmission = true;
        else
        {
            water.enableEmission = false;
        }

        // if (water.enableEmission == true && WaterCapacity > 0)
        //     WaterCapacity -= Time.deltaTime;
    }
}
