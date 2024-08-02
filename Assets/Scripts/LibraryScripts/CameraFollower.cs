using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform targetCamera;

    public float distance = 1.5f;
    
    public float rotationSpeed = 1.5f;
    public float positionSpeed = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        var fwd = targetCamera.forward;
        fwd.y = 0;
        fwd.Normalize();
        var targetRot = Quaternion.FromToRotation(Vector3.forward, fwd);
        var targetPos = targetCamera.position + fwd * distance;

        var rotFrac = Mathf.Exp(-rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(targetRot, transform.rotation, rotFrac);

        var posFrac = Mathf.Exp(-positionSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(targetPos, transform.position, posFrac);
    }
}
