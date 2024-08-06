using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterableEntityBehaviour : QuestableEntity
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private TextMeshPro WaterPercentUIPrefab;
    [SerializeField] private TextMeshPro WaterPercentUIComponent;
    [SerializeField] private Color NeutralStateColor = Color.white;
    [SerializeField] private Color GoodStateColor = Color.green;
    [SerializeField] private Color BadStateColor = Color.red;
    [SerializeField] private int GoodStateWaterPercentage = 100;
    [SerializeField] private int BadStateWaterPercentage = 150;
    [SerializeField] private int WaterPercent;

    [SerializeField] private int AllowableError = 20;
    [SerializeField] private int BlueWaterRequired;
    [SerializeField] private int RedWaterRequired;
    [SerializeField] private int GreenWaterRequired;
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
        //WaterPercentUIComponent = Instantiate(WaterPercentUIPrefab, transform);
        //WaterPercentUIComponent.color = NeutralStateColor;
    }

    // Update is called once per frame
    void Update()
    {
        WaterPercentUIComponent.text = $"{WaterPercent}%";
        WaterPercentUIComponent.transform.forward = mainCamera.transform.forward;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out CustomTag tags) && tags.HasTag("WaterCanParticles"))
        {
            WaterPercent += 1;

            if (IsCompletedPerfectly)
            {
                var water = other.GetComponent<WaterBehaviour>();
                
                IsCompletedPerfectly = Math.Abs(GreenWaterRequired - water.greenWater) <= AllowableError 
                                         && Math.Abs(BlueWaterRequired - water.blueWater) <= AllowableError
                                         && Math.Abs(RedWaterRequired - water.redWater) <= AllowableError 
                                         && WaterPercent < BadStateWaterPercentage;
            }

            if (WaterPercent <= GoodStateWaterPercentage)
            {
                WaterPercentUIComponent.color = Color.Lerp(NeutralStateColor, GoodStateColor,
                    WaterPercent / (float) GoodStateWaterPercentage);
            }
            else if (WaterPercent <= BadStateWaterPercentage)
            {
                WaterPercentUIComponent.color = Color.Lerp(GoodStateColor, BadStateColor,
                    (WaterPercent - GoodStateWaterPercentage) / 50f);
            }
        }
        
        IsCompleted = WaterPercent >= GoodStateWaterPercentage;
    }
}
