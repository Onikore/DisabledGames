using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 SnapToCardinalDirection(this Vector3 vec)
    {
        var x = Mathf.Abs(vec.x);
        var y = Mathf.Abs(vec.y);
        var z = Mathf.Abs(vec.z);

        if ((x >= y) && (x >= z))
            return new Vector3(Mathf.Sign(vec.x), 0, 0);
        else if ((y >= x) && (y >= z))
            return new Vector3(0, Mathf.Sign(vec.y), 0);
        else
            return new Vector3(0, 0, Mathf.Sign(vec.z));
    }
}
