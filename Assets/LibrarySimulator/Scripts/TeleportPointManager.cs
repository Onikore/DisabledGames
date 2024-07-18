using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointManager : MonoBehaviour
{
    public TeleportPoint InitialPoint;
    
    private HashSet<TeleportPoint> points = new HashSet<TeleportPoint>();

    private bool initialized = false;

    public void Attach(TeleportPoint point)
    {
        points.Add(point);
    }

    public void TeleportedToPoint(TeleportPoint point)
    {
        if (!points.Contains(point))
            return;

        foreach (var p in points)
            p.SetIndicatorEnabled(false);

        foreach (var p in point.GetConnectedPoints())
            if (points.Contains(p))
                p.SetIndicatorEnabled(true);
    }

    void LateUpdate()
    {
        if (initialized)
            return;

        if (InitialPoint != null)
        {
            points.Add(InitialPoint);
            TeleportedToPoint(InitialPoint);
        }

        initialized = true;
    }
}
