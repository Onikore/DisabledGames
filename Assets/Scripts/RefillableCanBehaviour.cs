using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RefillableCanBehaviour : MonoBehaviour
{
    [SerializeField] private SolutionMixerBehaviour water;

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out CustomTag tags))
        {
            if (tags.HasTag("BlueWater"))
            {
                water.AddWater(WaterType.Blue);
            }
            if (tags.HasTag("GreenWater"))
            {
                water.AddWater(WaterType.Green);
            }
            if (tags.HasTag("RedWater"))
            {
                water.AddWater(WaterType.Red);
            }
        }
    }
}
