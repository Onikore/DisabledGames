using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SolutionMixerBehaviour : QuestableEntity
{
    [SerializeField] private ParticleSystem water;
    [SerializeField] private double WaterCapacity = 5;
    [SerializeField] private AudioSource WaterSound;
    private Action DestroySoundAction;
    [SerializeField] private bool isDownside;
    [SerializeField] private GameObject Bottleneck;
    [SerializeField] private GameObject Bottom;

    [SerializeField] public int blueWater;
    [SerializeField] public int greenWater;
    [SerializeField] public int redWater;
    [SerializeField] public List<WaterType> RightWaterOrder;
    [SerializeField] public List<WaterType> CurrentWaterOrder;

    [SerializeField] private List<UnityEngine.UI.Image> Panels;

    private int currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        water = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Bottleneck.transform.position.y <= Bottom.transform.position.y)
        {
            water.enableEmission = true;
            if (!isDownside)
            {
                DestroySoundAction = SoundFXManager.Instance.PlaySoundFXClipUntilEventInvoke(WaterSound, transform, 1);
                isDownside = true;
            }
        }
        else
        {
            water.enableEmission = false;
            if (isDownside)
            {
                DestroySoundAction();
                isDownside = false;
            }
        }
    }

    public void ActivateNewPanel(WaterType waterType)
    {
        if (CurrentWaterOrder.Count == RightWaterOrder.Count)
            return;
        
        Panels[currentPanel].enabled = true;
        Panels[currentPanel].color = waterType switch
        {
            WaterType.Green => Color.green,
            WaterType.Blue => Color.blue,
            WaterType.Red => Color.red,
            _ => Panels[currentPanel].color
        };
        currentPanel++;
        CurrentWaterOrder.Add(waterType);
        
        if (CurrentWaterOrder.Count == RightWaterOrder.Count)
        {
            if (CurrentWaterOrder.SequenceEqual(RightWaterOrder))
            {
                Panels.ForEach(x => x.color = Color.green);
                IsCompleted = true;
            }
            else
            {
                Panels.ForEach(x => x.color = Color.red);
            }
        }
    }

    public void AddWater(WaterType waterType)
    {
        switch (waterType)
        {
            case WaterType.Green:
                greenWater += 2;
                if (greenWater % 100 == 0)
                    ActivateNewPanel(WaterType.Green);
                break;
            case WaterType.Blue:
                blueWater += 2;
                if (blueWater % 100 == 0)
                    ActivateNewPanel(WaterType.Blue);
                break;
            case WaterType.Red:
                redWater += 2;
                if (redWater % 100 == 0)
                    ActivateNewPanel(WaterType.Red);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(waterType), waterType, null);
        }
    }

    public void ResetPanels()
    {
        blueWater = 0;
        redWater = 0;
        greenWater = 0;
        currentPanel = 0;
        CurrentWaterOrder.Clear();
        foreach (var panel in Panels)
        {
            panel.enabled = false;
        }
    }
}
