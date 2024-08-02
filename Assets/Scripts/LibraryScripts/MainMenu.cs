using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject main, settings;

    public Storyline story;

    public float menuFadeInSeconds;
    public RectTransform taskSelectPanel;
    public UnityEngine.Rendering.Volume bgVolume;

    private bool open = true;
    private float fadeInFrac = 0;
    private float taskSelectHeight;

    private float openDelay = -1;

    public void StartGameClicked()
    {
        open = false;
        DoStartGame();
    }

    public void SettingsClicked()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void SettingsBackClicked()
    {
        main.SetActive(true);
        settings.SetActive(false);
    }

    public void VolumeChanged(float v)
    {
        
    }

    void DoStartGame()
    {
        story.Begin(() => { openDelay = 0.5f; });
    }

    void DoStopGame()
    {
        story.ResetStory();
    }

    // Start is called before the first frame update
    void Start()
    {
        taskSelectHeight = taskSelectPanel.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            openDelay = 0.1f;
            DoStopGame();
        }

        if (openDelay > 0)
        {
            openDelay -= Time.deltaTime;
            if (openDelay <= 0)
            {
                open = true;
                openDelay = -1;
                OpenMenu();
            }
        }

        var delta = Time.deltaTime / menuFadeInSeconds;
        float newValue;
        if (open)
            newValue = Mathf.Min(fadeInFrac + delta, 1f);
        else
            newValue = Mathf.Max(fadeInFrac - delta, 0f);
        
        if (fadeInFrac == newValue)
            return;
        fadeInFrac = newValue;

        var height = taskSelectHeight * fadeInFrac;
        if (Mathf.Abs(taskSelectPanel.rect.height - height) > 0.01f)
            taskSelectPanel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

        bgVolume.weight = fadeInFrac;

        if (fadeInFrac > 0)
            return;

        CloseMenu();
    }

    private void OpenMenu()
    {
        main.SetActive(true);
        settings.SetActive(false);
    }

    private void CloseMenu()
    {
        main.SetActive(false);
        settings.SetActive(false);
    }
}
