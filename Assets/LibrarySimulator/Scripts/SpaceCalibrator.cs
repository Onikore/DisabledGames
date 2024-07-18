using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCalibrator : MonoBehaviour
{
    public Transform xrOffset;
    public Transform xrCamera;
    public Transform target;

    void Start()
    {
        LoadOffset();
        // Invoke("LoadOffset", 0.5f);
    }

    public void SetOffset()
    {
        Debug.Log("set offset");

        var fwd1 = xrCamera.forward;
        fwd1.y = 0;
        fwd1.Normalize();

        var fwd2 = target.forward;
        fwd2.y = 0;
        fwd2.Normalize();

        var rotOffset = Quaternion.FromToRotation(fwd1, fwd2);
        xrOffset.rotation *= rotOffset;

        var posOffset = target.position - xrCamera.position;
        xrOffset.position += posOffset;

        SaveOffset();
    }

    private void SaveOffset()
    {
        var pos = xrOffset.localPosition;
        var rot = xrOffset.localRotation;
        
        var saved = new SavedInfo();
        saved.pose = new Pose(pos, rot);

        var s = JsonUtility.ToJson(saved);
        Debug.Log("Saved pose: " + s);
        PlayerPrefs.SetString("SavedPose", s);
    }

    private void LoadOffset()
    {
        var s = PlayerPrefs.GetString("SavedPose", "");
        Debug.Log("Loading saved: " + s);

        var saved = JsonUtility.FromJson<SavedInfo>(s);
        Debug.Log("Loaded saved info: " + saved);
        Debug.Log("Loaded pose: " + saved.pose);
        if (saved == null)
            return;

        xrOffset.localPosition = saved.pose.position;
        xrOffset.localRotation = saved.pose.rotation;
    }

    private class SavedInfo
    {
        public Pose pose;
    }
}
