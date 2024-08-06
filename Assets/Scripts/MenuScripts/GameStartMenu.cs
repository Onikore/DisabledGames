using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;
    public GameObject sceneSelect;

    [Header("Main Menu Buttons")]
    public Button startOfficeScene;
    public Button sceneSelectButton;
    public Button startLibraryScene;
    public Button optionButton;
    public Button aboutButton;
    public Button quitButton;
    public Button mainMenuButton;


    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableMainMenu();

        //Hook events
        startOfficeScene.onClick.AddListener(StarOfficeGame);
        sceneSelectButton.onClick.AddListener(EnableSceneSelect);
        startLibraryScene.onClick.AddListener(StartLibraryGame);
        optionButton.onClick.AddListener(EnableOption);
        aboutButton.onClick.AddListener(EnableAbout);
        quitButton.onClick.AddListener(QuitGame);
        mainMenuButton.onClick.AddListener(EnableMainMenu);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableMainMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StarOfficeGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void StartLibraryGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(2);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        sceneSelect.SetActive(false);

    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
        sceneSelect.SetActive(false);
    }
    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
        sceneSelect.SetActive(false);
    }
    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
        sceneSelect.SetActive(false);
    }

    public void EnableSceneSelect()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
        sceneSelect.SetActive(true);
    }
}
