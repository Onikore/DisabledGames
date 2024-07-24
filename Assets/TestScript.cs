using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TestScript : MonoBehaviour
{
    bool checkpoint = false;
    // Start is called before the first frame update
    void Start()
    {
        
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => Grab());
        grabInteractable.deactivated.AddListener(x => Ungrab());

    }


    public void Grab()
    {
        if (!checkpoint)
        {
            checkpoint = true;
            AudioManager.instance.Play("GrabTablet"); 
        }

    }

    public void Ungrab()
    {
     
    }


}
