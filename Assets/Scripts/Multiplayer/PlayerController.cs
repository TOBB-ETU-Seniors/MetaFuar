using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private PhotonView view;
    [SerializeField]
    private GameObject m_camera;

    void Start()
    {
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
            m_camera.SetActive(false);

    }
}
