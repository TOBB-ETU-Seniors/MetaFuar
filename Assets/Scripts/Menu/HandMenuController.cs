using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandMenuController : MonoBehaviour
{
    // Reference to the hand menu GameObject
    public GameObject handMenu;

    void Update()
    {
        // Check if the menu button on the Oculus controller has been pressed
        if (OVRInput.GetDown(OVRInput.Button.Two) && SceneManager.GetActiveScene().name != "Menu")
        {
            handMenu.SetActive(!handMenu.activeSelf);
        }
    }

}
