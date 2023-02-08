using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordVisibilityManager : MonoBehaviour
{
    public Button passwordVisibilityButton;
    public TMP_InputField passwordField;
    public Sprite showPasswordImage;
    public Sprite hidePasswordImage;
    
    private void Awake()
    {
        passwordVisibilityButton = GetComponent<Button>();
        passwordField = GetComponentInParent<TMP_InputField>();
        passwordVisibilityButton.onClick.AddListener(ShowHidePassword);
    }

    private void ShowHidePassword()
    {
        if (passwordField.contentType == TMP_InputField.ContentType.Standard)
        {
            passwordField.contentType = TMP_InputField.ContentType.Password;
            passwordVisibilityButton.GetComponent<Image>().sprite = showPasswordImage;
        }
        else
        {
            passwordField.contentType = TMP_InputField.ContentType.Standard;
            passwordVisibilityButton.GetComponent<Image>().sprite = hidePasswordImage;
        }
        passwordField.Select();
    }
}
