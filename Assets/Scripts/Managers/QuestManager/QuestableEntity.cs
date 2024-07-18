using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class QuestableEntity : MonoBehaviour
{
    private bool isCompleted;
    
    [SerializeField]
    public bool IsCompleted
    {
        get => isCompleted;
        set
        {
            isCompleted = value;
            
            if (isCompleted)
            {
                OnQuestCompleted?.Invoke(this);
            }
        }
    }
    [SerializeField] public bool IsCompletedPerfectly = true;

    public event Action<QuestableEntity> OnQuestCompleted;
}
