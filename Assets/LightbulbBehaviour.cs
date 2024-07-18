using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbBehaviour : QuestableEntity
{
    [SerializeField] private Material LightMaterial;
    [SerializeField] private Light LightComponent;
    [SerializeField] private Color OffColor;
    [SerializeField] private Color OnColor;
    [SerializeField] private bool IsActivated;

    public void Start()
    {
        LightMaterial.color = OffColor;
    }

    public void TurnOn()
    {
        if (!IsActivated)
            TurnOnLight();
        else 
            TurnOffLight();
    }

    private void TurnOnLight()
    {
        IsActivated = true;
        LightComponent.enabled = true;
        LightMaterial.color = OnColor;
        IsCompleted = true;
    }

    private void TurnOffLight()
    {
        IsActivated = false;
        LightComponent.enabled = false;
        LightMaterial.color = OffColor;
        IsCompleted = false;
    }
}
