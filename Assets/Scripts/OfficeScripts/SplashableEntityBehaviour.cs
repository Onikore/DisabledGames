using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SplashableEntityBehaviour : QuestableEntity
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
    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
        //WaterPercentUIComponent = Instantiate(WaterPercentUIPrefab, transform);
       // WaterPercentUIComponent.color = NeutralStateColor;
    }

    // Update is called once per frame
    void Update()
    {
        WaterPercentUIComponent.text = $"{WaterPercent}%";
        WaterPercentUIComponent.transform.forward = mainCamera.transform.forward;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out CustomTag tags) && tags.HasTag("AtomizerParticles"))
        {
            WaterPercent += 1;
            
            IsCompleted = WaterPercent >= GoodStateWaterPercentage;
            IsCompletedPerfectly = WaterPercent < BadStateWaterPercentage;
            
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
    }
}
