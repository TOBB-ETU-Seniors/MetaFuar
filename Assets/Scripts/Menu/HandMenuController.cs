using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandMenuController : MonoBehaviour
{
    // Reference to the hand menu GameObject
    public GameObject handMenu;

    private void OnEnable()
    {
        handMenu.SetActive(false);
    }

    void Update()
    {
        // Check if the menu button on the Oculus controller has been pressed
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "LoginScene")
            {
                handMenu.SetActive(!handMenu.activeSelf);
            }
        }
    }

}
