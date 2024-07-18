using System;
using UnityEngine;

public class TaskBase : MonoBehaviour
{
    public event Action onTaskCompleted;

    protected void TaskCompleted()
    {
        onTaskCompleted?.Invoke();
    }
}
