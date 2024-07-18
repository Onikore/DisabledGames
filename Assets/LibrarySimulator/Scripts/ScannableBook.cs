using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]
public class ScannableBook : MonoBehaviour
{
    [HideInInspector]
    public BookScanner currentScanner;

    public Animator OpenAnimator { get; private set; }

    public bool IsGrabbed { get ; private set; }

    private Rigidbody rb;
    private XRGrabInteractable interactable;

    public void SetGrabbable(bool grabbable)
    {
        rb.isKinematic = !grabbable;
        interactable.enabled = grabbable;
    }

    void Start()
    {
        OpenAnimator = GetComponent<Animator>();
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        interactable.selectEntered
            .AddListener(new UnityAction<SelectEnterEventArgs>(OnGrabbed));
        interactable.selectExited
            .AddListener(new UnityAction<SelectExitEventArgs>(OnReleased));
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Grabbed", this);
        IsGrabbed = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        Debug.Log("Released", this);
        currentScanner?.OnBookReleased(this);
        IsGrabbed = false;
    }
}
