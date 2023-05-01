using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMenuController : MonoBehaviour
{
    Vector3 rotation_offset = new Vector3(0, 1, 0);
    public GameObject panel;

    void Start()
    {
        this.panel.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        // character triggers when get close
        if (collider.gameObject.tag != "Character")
            return;
        this.panel.SetActive(true);
    }

    void OnTriggerExit(Collider collider)
    {
        // character triggers when get close
        if (collider.gameObject.tag != "Character")
            return;
        Debug.Log("Character exited...");
        this.panel.SetActive(false);
    }
}
