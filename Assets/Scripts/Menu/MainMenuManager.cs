using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;

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
    [Tooltip("The UI Panel holding the Inventory Panel elements")]
    public GameObject inventoryPanel;
    [Tooltip("The UI Panel holding the about")]
    public GameObject aboutPanel;
    [Tooltip("The UI Panel holding the Exit Panel elements")]
    public GameObject exitPanel;
    [Tooltip("The Loading Screen holding loading bar")]
    public GameObject loadingScreen;

    [Header("Date")]
    [Tooltip("The date and time display text at the bottom of the screen")]
    public TMP_Text dateDisplay;
    public TMP_Text timeDisplay;
    public bool showDate = true;
    public bool showTime = true;

    [Header("Scene")]
    public GameObject gameController;
    [Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
    public static string newSceneName { get; set; } = "Fuar";
    public static int spawnIndex { get; set; } = 0;

    [Header("Debug")]
    Transform tempParent;
    public bool SetActiveMainMenuPanelOnStart = true;

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
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
        if (aboutPanel != null)
            aboutPanel.SetActive(false);
        if (exitPanel != null)
            exitPanel.SetActive(false);
        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        gameController = GameObject.Find("GameController");

        newSceneName = "Fuar";
        spawnIndex = 0;
    }

    // Just for reloading the scene! You can delete this function entirely if you want to
    void Update()
    {
        // Clock/Date Elements
        DateTime time = DateTime.Now;
        if (showTime) { timeDisplay.text = time.ToString("HH:mm:ss"); } else if (!showTime) { timeDisplay.text = ""; }
        if (showDate) { dateDisplay.text = time.ToString("yyyy/MM/dd"); } else if (!showDate) { dateDisplay.text = ""; }
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
        StartCoroutine(LoadAsynchronously(newSceneName));
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

    public void JoinScene()
    {
        JoinToRoom.SceneName = newSceneName;
        JoinToRoom.spawnIndex = spawnIndex;
        StartCoroutine(JoinSceneAsynchronously());
    }

    IEnumerator JoinSceneAsynchronously()
    {
        gameController.GetComponent<JoinToRoom>().JoinRoom();

        Slider loadingBar = loadingScreen.GetComponentInChildren<Slider>();

        float temp = 0f;

        while (PhotonNetwork.LevelLoadingProgress != 1)
        {
            float progress = Mathf.Clamp01((PhotonNetwork.LevelLoadingProgress + temp) / .9f);
            loadingBar.value = progress;

            temp += 0.02f;

            yield return null;
        }

        loadingBar.value = 1f;
    }

}
