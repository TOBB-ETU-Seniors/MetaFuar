using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class HandMenuManager : MonoBehaviour
{

    #region VARIABLES

    [Header("Simple Panels")]
    [Tooltip("The UI Panel holding the Main Menu Panel elements")]
    public GameObject handMenuPanel;
    [Tooltip("The UI Panel holding the Settings Panel elements")]
    public GameObject settingsPanel;
    [Tooltip("The UI Panel holding the Explore Panel elements")]
    public GameObject explorePanel;
    [Tooltip("The UI Panel holding the Inventory Panel elements")]
    public GameObject inventoryPanel;
    [Tooltip("The UI Panel holding the Exit Panel elements")]
    public GameObject exitPanel;
    [Tooltip("The Loading Screen holding loading bar")]
    public GameObject loadingScreen;

    [Header("Scene")]
    public GameObject gameController;
    [Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
    public static string newSceneName { get; set; } = "Fuar";
    public static int spawnIndex { get; set; } = 0;

    [Header("Debug")]
    Transform tempParent;
    public bool SetActiveHandMenuPanelOnStart = true;

    private GameObject oVRPlayer;
    private PhotonView photonView;
    private CharacterController characterController;

    #endregion
    private void OnEnable()
    {
        // By default, starts on the main menu panel, disables others
        if (SetActiveHandMenuPanelOnStart)
            handMenuPanel.SetActive(true);
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (explorePanel != null)
            explorePanel.SetActive(false);
        if (inventoryPanel != null)
            inventoryPanel.SetActive(false);
        if (exitPanel != null)
            exitPanel.SetActive(false);
        if (loadingScreen != null)
            loadingScreen.SetActive(false);

        gameController = GameObject.Find("GameController");

        oVRPlayer = GameObject.FindGameObjectWithTag("Player");

        photonView = oVRPlayer.GetComponent<PhotonView>();
        characterController = oVRPlayer.GetComponent<CharacterController>();
    }

    public void MoveToFront(GameObject currentObj)
    {
        //tempParent = currentObj.transform.parent;
        tempParent = currentObj.transform;
        tempParent.SetAsLastSibling();
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
        if (newSceneName == SceneManager.GetActiveScene().name)
        {
            if (photonView.IsMine)
            {
                characterController.Move(GameObject.Find("SpawnPoints").GetComponent<SpawnPlayers>().spawnPoints[spawnIndex].position - oVRPlayer.transform.position);
            }
        }
        else if (newSceneName == "Menu")
        {
            PhotonNetwork.LeaveRoom();
            LoadNewScene();
        }
        else
        {
            StartCoroutine(JoinSceneAsynchronously());
        }
    }

    IEnumerator JoinSceneAsynchronously()
    {
        /*        PhotonNetwork.LeaveRoom();
                PhotonNetwork.ConnectUsingSettings();

                gameController.GetComponent<JoinToRoom>().JoinRoom();*/

        PhotonNetwork.LoadLevel(newSceneName);

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
