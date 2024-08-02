using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabletUISystem : MonoBehaviour
{
    public RectTransform menuPanel, hintsPanel, cameraPanel, settingsPanel;
    public Button backButton;

    private HintManager hintManager;

    void Start()
    {
        hintManager = GetComponent<HintManager>();
    }

    public void BackToMenu()
    {
        DisableAllPanels();
        menuPanel.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }

    public void OpenHints()
    {
        DisableAllPanels();
        hintsPanel.gameObject.SetActive(true);
        hintManager.HintScreenOpened();
    }

    public void OpenCamera()
    {
        DisableAllPanels();
        cameraPanel.gameObject.SetActive(true);
    }

    public void OpenSettings()
    {
        DisableAllPanels();
        settingsPanel.gameObject.SetActive(true);
    }

    private void DisableAllPanels()
    {
        backButton.gameObject.SetActive(true);
        menuPanel.gameObject.SetActive(false);
        hintsPanel.gameObject.SetActive(false);
        cameraPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
    }
}
