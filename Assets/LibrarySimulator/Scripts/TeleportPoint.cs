using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(TeleportationAnchor))]
public class TeleportPoint : MonoBehaviour
{
    public TeleportPointManager Manager;

    public TeleportPoint[] ConnectedPoints;

    public GameObject Indicator;

    public void SetIndicatorEnabled(bool enabled)
    {
        Indicator.SetActive(enabled);
    }

    public IEnumerable<TeleportPoint> GetConnectedPoints()
    {
        return ConnectedPoints;
    }

    void Start()
    {
        if (Manager == null)
            Manager = FindObjectOfType<TeleportPointManager>();

        if (Manager == null)
            return;

        Manager.Attach(this);
        GetComponent<TeleportationAnchor>().teleporting
            .AddListener(
                new UnityAction<TeleportingEventArgs>(OnTeleporting)
             );
    }

    void OnDrawGizmosSelected()
    {
        var selfPos = transform.position;

        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.HSVToRGB(0.1f, 0.9f, 1.0f);
        foreach (var p in ConnectedPoints)
            if (p != null)
                Gizmos.DrawLine(selfPos, p.transform.position);
    }

    private void OnTeleporting(TeleportingEventArgs args)
    {
        Manager.TeleportedToPoint(this);
    }
}
