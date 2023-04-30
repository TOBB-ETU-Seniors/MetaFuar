using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class LoginManager : MonoBehaviour
{
    public enum InputType
    {
        Username = 0,
        Email = 1,
        Password = 2,
        EmailOrUsername = 3,
    }

    [Serializable]
    public struct InputField
    {
        public TMP_InputField inputField;
        public InputType inputType;
    }

    #region VARIABLES

    [Header("Simple Panels")]
    [Tooltip("The UI Panel holding the Home Panel elements")]
    public GameObject homePanel;
    [Tooltip("The UI Panel holding the Login Panel elements")]
    public GameObject loginPanel;
    [Tooltip("The UI Panel holding the Register Panel elements")]
    public GameObject registerPanel;
    [Tooltip("The UI Panel holding the Email Login Panel elements")]
    public GameObject emailLoginPanel;
    [Tooltip("The UI Panel holding the Token Login Panel elements")]
    public GameObject tokenLoginPanel;
    [Tooltip("The UI Panel holding the Continue Without Account Panel elements")]
    public GameObject continueWithoutAccountPanel;
    [Tooltip("The UI Panel holding the Email Register Panel elements")]
    public GameObject emailRegisterPanel;
    [Tooltip("The UI Panel holding the Username Form Panel elements")]
    public GameObject usernameFormPanel;
    [Tooltip("The UI Panel holding the Exit Panel elements")]
    public GameObject exitPanel;
    [Tooltip("The Loading Screen holding loading bar")]
    public GameObject loadingScreen;


    [Header("COLORS - Tint")]
    public Image[] panelGraphics;
    public Image[] blurs;
    public Color tint;

    [Tooltip("The name of the scene loaded when a 'NEW GAME' is started")]
    public string newSceneName;

    [Header("Form Inputs")]
    public InputField[] inputFields;

    bool registerFormIsValid;

    InputValidator inputValidator;

    #endregion

    void Start()
    {
        // By default, starts on the home panel, disables others
        homePanel.SetActive(true);
        if (loginPanel != null)
            loginPanel.SetActive(false);
        if (registerPanel != null)
            registerPanel.SetActive(false);
        if (emailLoginPanel != null)
            emailLoginPanel.SetActive(false);
        if (continueWithoutAccountPanel != null)
            continueWithoutAccountPanel.SetActive(false);
        if (emailRegisterPanel != null)
            emailRegisterPanel.SetActive(false);
        if (usernameFormPanel != null)
            loadingScreen.SetActive(false);
        if (usernameFormPanel != null)
            loginPanel.SetActive(false);
        if (exitPanel != null)
            exitPanel.SetActive(false);
        if (loadingScreen != null)
            loadingScreen.SetActive(false);
        if (tokenLoginPanel != null)
            tokenLoginPanel.SetActive(false);

        inputValidator = new InputValidator();

        InitializeEventListeners();
    }

    void Update()
    {

    }

    #region LOGIN

    public void LoginWithEmail()
    {
        TMP_InputField[] inputFields = emailLoginPanel.GetComponentsInChildren<TMP_InputField>();
        string usernameOrEmailText = inputFields[0].text;
        string passwordText = inputFields[1].text;
        Debug.Log("Email ile giriþ yapýldý.\n" +
                "Kullanýcý Adý / E-Posta: " + usernameOrEmailText + "\n" +
                "Þifre: " + passwordText);
    }

    public void LoginWithGoogle()
    {
        Debug.Log("Google ile giriþ yapýldý.");
    }

    public void LoginWithFacebook()
    {
        Debug.Log("Facebook ile giriþ yapýldý.");
    }

    public void LoginWithToken()
    {
        string token = tokenLoginPanel.GetComponentInChildren<TMP_InputField>().text;
        Debug.Log("Token: " + token + " ile giriþ yapýlýyor.");
        StartCoroutine(loginBackend.LogInWithToken(token));
    }

    public void ContinueWithoutAccount()
    {
        string usernameText = continueWithoutAccountPanel.GetComponentInChildren<TMP_InputField>().text;
        Debug.Log("Misafir olarak giriþ yapýldý.\n" +
                "Kullanýcý Adý: " + usernameText);
    }



    #endregion

    #region REGISTER

    public void RegisterWithEmail()
    {
        TMP_InputField[] inputFields = emailRegisterPanel.GetComponentsInChildren<TMP_InputField>();
        string usernameText = inputFields[0].text;
        string emailText = inputFields[1].text;
        string passwordText = inputFields[2].text;

        Debug.Log("Email ile kayýt olundu.\n" +
                "Kullanýcý adý: " + usernameText + "\n" +
                "E-Posta: " + emailText + "\n" +
                "Þifre: " + passwordText);
    }

    public void RegisterWithGoogle()
    {
        Debug.Log("Google ile kayýt olundu.");
    }
    public void RegisterWithFacebook()
    {
        Debug.Log("Facebook ile kayýt olundu.");
    }

    public void AddUsernameToAccount()
    {
        string usernameText = continueWithoutAccountPanel.GetComponentInChildren<TMP_InputField>().text;
        Debug.Log("Misafir olarak giriþ yapýldý.\n" +
                "Kullanýcý Adý: " + usernameText);
    }

    #endregion

    #region Scene Management

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
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        Slider loadingBar = loadingScreen.GetComponentInChildren<Slider>();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingBar.value = progress;

            yield return null;
        }
    }

    #endregion

    #region Events

    private void InitializeEventListeners()
    {
        foreach (InputField inputField in inputFields)
        {
            inputField.inputField.onDeselect.AddListener((value) => OnInputDeselected(value, inputField.inputType, inputField.inputField));
        }
    }

    private void OnInputDeselected(string value, InputType inputType, TMP_InputField inputField)
    {
        string errorMessage;
        switch (inputType)
        {
            case InputType.Username:
                errorMessage = inputValidator.ValidateUsername(value);
                break;
            case InputType.Email:
                errorMessage = inputValidator.ValidateEmail(value);
                break;
            case InputType.Password:
                errorMessage = inputValidator.ValidatePassword(value);
                break;
            case InputType.EmailOrUsername:
                errorMessage = inputValidator.ValidateEmailOrUsername(value);
                break;
            default:
                errorMessage = "";
                break;
        }

        GameObject inputFieldParent = inputField.transform.parent.gameObject;

        TMP_Text errorText = inputFieldParent.transform.Find("ErrorText").GetComponent<TMP_Text>();
        if (errorText != null)
            errorText.text = errorMessage;

        Image inputFieldImage = inputField.GetComponent<Image>();
        if (errorMessage != "" && inputFieldImage != null)
        {
            registerFormIsValid = false;
            inputFieldImage.color = new Color32(255, 150, 150, 255);
        }
        else
        {
            registerFormIsValid = true;
            inputFieldImage.color = new Color32(255, 255, 255, 255);
        }

    }

    #endregion

}
