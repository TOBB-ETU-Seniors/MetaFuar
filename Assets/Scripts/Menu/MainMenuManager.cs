using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class MainMenuManager : MonoBehaviour
{

    #region VARIABLES

    [Header("Simple Panels")]
    [Tooltip("The UI Panel holding the Main Menu Panel elements")]
    public GameObject mainMenuPanel;
    [Tooltip("The UI Panel holding the Settings Panel elements")]
    public GameObject settingsPanel;
    [Tooltip("The UI Panel holding the Explore Panel elements")]
    public GameObject explorePanel;
    [Tooltip("The UI Panel holding the about")]
    public GameObject aboutPanel;
    [Tooltip("The UI Panel holding the Exit Panel elements")]
    public GameObject exitPanel;
    [Tooltip("The UI Panel holding the Hand Menu Exit Panel elements")]
    public GameObject exitPanel_Hand;
    [Tooltip("The Loading Screen holding loading bar")]
    public GameObject loadingScreen;

    [Header("Date")]
    [Tooltip("The date and time display text at the bottom of the screen")]
    public TMP_Text dateDisplay;
    public TMP_Text timeDisplay;
    public bool showDate = true;
    public bool showTime = true;

    [Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
    public string NewSceneName { get; set; }

    [Header("Debug")]
    Transform tempParent;
    public bool SetActiveMainMenuPanelOnStart = true;

    public LocalizedString mainMenuTitle;
    public LocalizedString handMenuTitle;

    [Header("Hand Menu")]
    public bool HandMenu = false;
    private bool HandMenu_Old = false;
    private bool isMenuAdjusted = false;

    [Header("Different Buttons in Main Menu and Hand Menu")]
    public GameObject MenuTitleObject;
    public GameObject Btn_EnterShowroom;
    public GameObject Btn_About;
    public GameObject Btn_Exit;
    public GameObject Btn_Exit_Hand;

    private GameObject gameController;

    #endregion
    private void OnEnable()
    {
        // By default, starts on the main menu panel, disables others
        if (SetActiveMainMenuPanelOnStart)
            mainMenuPanel.SetActive(true);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (explorePanel != null)
            explorePanel.SetActive(false);
        if (aboutPanel != null)
            aboutPanel.SetActive(false);
        if (exitPanel != null)
            exitPanel.SetActive(false);
        if (exitPanel_Hand != null)
            exitPanel_Hand.SetActive(false);
        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        if (!isMenuAdjusted)
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                HandMenu = false;
                MainMenuAdjustments();
            }
            else
            {
                HandMenu = true;
                HandMenuAdjustments();
            }
            isMenuAdjusted = true;
        }

        gameController = GameObject.Find("GameController");
    }

    // Just for reloading the scene! You can delete this function entirely if you want to
    void Update()
    {
        if (HandMenu != HandMenu_Old)
        {
            HandMenu_Old = HandMenu;
            isMenuAdjusted = false;
        }

        if (!isMenuAdjusted)
        {
            if (HandMenu)
                HandMenuAdjustments();
            else
                MainMenuAdjustments();
            isMenuAdjusted = true;
        }

        // Clock/Date Elements
        DateTime time = DateTime.Now;
        if (showTime) { timeDisplay.text = time.ToString("HH:mm:ss"); } else if (!showTime) { timeDisplay.text = ""; }
        if (showDate) { dateDisplay.text = time.ToString("yyyy/MM/dd"); } else if (!showDate) { dateDisplay.text = ""; }
    }

    void MainMenuAdjustments()
    {
        gameObject.layer = LayerMask.NameToLayer("Transparent_UI");

        if (MenuTitleObject != null)
            MenuTitleObject.GetComponent<LocalizeStringEvent>().StringReference = mainMenuTitle;
        if (Btn_EnterShowroom != null)
            Btn_EnterShowroom.SetActive(true);
        if(Btn_About != null)
            Btn_About.SetActive(true);
        if(Btn_Exit != null)
            Btn_Exit.SetActive(true);
        if (Btn_Exit_Hand != null)
            Btn_Exit_Hand.SetActive(false);

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
    }

    void HandMenuAdjustments()
    {
        gameObject.layer = LayerMask.NameToLayer("UI");

        if (MenuTitleObject != null)
            MenuTitleObject.GetComponent<LocalizeStringEvent>().StringReference = handMenuTitle;
        if (Btn_EnterShowroom != null)
            Btn_EnterShowroom.SetActive(false);
        if (Btn_About != null)
            Btn_About.SetActive(false);
        if (Btn_Exit != null)
            Btn_Exit.SetActive(false);
        if (Btn_Exit_Hand != null)
            Btn_Exit_Hand.SetActive(true);

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1250, 1080);
    }

    public void MoveToFront(GameObject currentObj)
    {
        //tempParent = currentObj.transform.parent;
        tempParent = currentObj.transform;
        tempParent.SetAsLastSibling();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Called when loading new scene
    public void LoadNewScene()
    {
        StartCoroutine(LoadAsynchronously(NewSceneName));
    }

    // Load Bar synching animation
    IEnumerator LoadAsynchronously(string sceneName)
    { // scene name is just the name of the current scene being loaded
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        Slider loadingBar = loadingScreen.GetComponentInChildren<Slider>();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;

            yield return null;
        }
    }

    /*public void joinScene(int spawnPointIndex)
    {
        JoinToRoom.SceneName = NewSceneName;
        JoinToRoom.spawnIndex = spawnPointIndex;
        gameController.GetComponent<JoinToRoom>().JoinRoom();
    }*/


}
