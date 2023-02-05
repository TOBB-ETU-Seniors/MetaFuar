using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MouseLook : MonoBehaviour
{

    public float mouseSensivity = 100f;
    public Transform playerBody;
    //float xRotation = 0f;
    private PhotonView view;


    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;
    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        view = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeScale == 0f) return;

        if (view.IsMine)
        {

            mouseX = Input.GetAxisRaw("Mouse X");
            mouseY = Input.GetAxisRaw("Mouse Y");

            yRotation += mouseX * sensX * multiplier;
            xRotation -= mouseY * sensY * multiplier;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }        
    }
}
