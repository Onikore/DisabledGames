using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LeafBehaviour : QuestableEntity
{
    [SerializeField] private Outline outline;
    [SerializeField] public float fallSpeed = 0.2f;
    [SerializeField] public float swayAmplitude = 0.5f;
    [SerializeField] public float swayFrequency = 1.0f;
    [SerializeField] public float rotationSpeed = 30.0f;
    [SerializeField] public bool isFalling;
    [SerializeField] private float initialX;
    [SerializeField] private float initialTime;
    [SerializeField] private bool isHealthy;
    
    void Start()
    {
        initialX = transform.position.x;  // Сохраняем начальную позицию по X
    }

    void Update()
    {
        if (isFalling && transform.position.y > 0)
        {
            if (initialTime == 0)
                initialTime = Time.time;
            transform.position += Vector3.down * (fallSpeed * Time.deltaTime);
            
            float elapsedTime = Time.time - initialTime;
            
            float sway = Mathf.Sin(elapsedTime * swayFrequency) * swayAmplitude;
            transform.position = new Vector3(initialX + sway, transform.position.y, transform.position.z);
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isFalling && other.transform.TryGetComponent<ScissorsBehaviour>(out var scissors))
        {
            outline.enabled = true;
            scissors.LeafInContact = this;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<ScissorsBehaviour>(out var scissors))
        {
            outline.enabled = false;
            scissors.LeafInContact = null;
        }
    }

    public void FallFromStem()
    {
        isFalling = true;
        IsCompleted = true;
        if (isHealthy)
            IsCompletedPerfectly = false;
    }
}
