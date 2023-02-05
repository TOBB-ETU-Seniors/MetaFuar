using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;    
    public LayerMask groundMask;

    [SerializeField]
    private GameObject m_camera;

    bool isGrounded;
    Vector3 velocity;

    public float groundDistance = 0.4f;
    public float speed = 12f;
    public float gravity = -9.81f;


    private PhotonView view;


    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(view.IsMine)
        {
            MoveCharacter();
        }
        else
        {
            m_camera.SetActive(false);
        }

    }

    private void MoveCharacter()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity + Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


}
