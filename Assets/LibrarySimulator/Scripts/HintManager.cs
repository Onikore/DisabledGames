using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public float notificationAnimTime = 1f;
    public float extendNotificationHeight = 0f;

    public AudioSource notificationSound;
    public RectTransform notificationPanel;
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI hintScreenText;

    private float notifAnimDirection = 0f;
    private float notifAnimFrac = 0f;

    public void SetHint(string text)
    {
        notificationText.text = text;
        hintScreenText.text = text;
        notifAnimDirection = (text.Length > 0) ? 1 : -1;
        if (text.Length > 0)
            notificationSound.Play();
    }

    public void HintScreenOpened()
    {
        notifAnimDirection = -1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHint("");
    }

    // Update is called once per frame
    void Update()
    {
        var f = notifAnimFrac;
        f += notifAnimDirection * Time.deltaTime / notificationAnimTime;
        f = Mathf.Clamp01(f);
        if (f == notifAnimFrac)
            return;

        notifAnimFrac = f;

        notificationPanel.localPosition = Vector3.up * extendNotificationHeight * notifAnimFrac;
    }
}
