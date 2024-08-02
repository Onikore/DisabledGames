// Credit: https://communityforums.atmeta.com/t5/Unity-VR-Development/Meta-XR-Simulator-starts-only-once/td-p/1141806
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.XR.Management;
using System.Collections;
#endif

public class XRInitFixer : MonoBehaviour
{

#if UNITY_EDITOR
    private void Start()
    {
        EnableXR();
    }

    private void OnDestroy()
    {
        DisableXR();
    }

    public void EnableXR()
    {
        StartCoroutine(StartXRCoroutine());
    }

    public void DisableXR()
    {
        XRGeneralSettings.Instance?.Manager?.StopSubsystems();
        XRGeneralSettings.Instance?.Manager?.DeinitializeLoader();
    }

    public IEnumerator StartXRCoroutine()
    {
        if (XRGeneralSettings.Instance == null)
        {
            XRGeneralSettings.Instance = XRGeneralSettings.CreateInstance<XRGeneralSettings>();
        }

        if (XRGeneralSettings.Instance.Manager == null)
        {
            yield return new WaitUntil(() => XRGeneralSettings.Instance.Manager != null);
        }

        XRGeneralSettings.Instance?.Manager?.InitializeLoaderSync();

        if (XRGeneralSettings.Instance?.Manager?.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            XRGeneralSettings.Instance?.Manager?.StartSubsystems();
        }
    }

#endif

}