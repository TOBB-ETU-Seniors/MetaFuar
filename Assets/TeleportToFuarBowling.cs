using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class TeleportToFuarBowling : MonoBehaviour
{
    private string newSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Teleport()
    {
        if (SceneManager.GetActiveScene().name == "Fuar")
        {
            JoinToRoom.SceneName = "Bowling";
            newSceneName = "Bowling";
        }
        else
        {
            JoinToRoom.SceneName = "Fuar";
            newSceneName = "Fuar";
        }

        StartCoroutine(JoinSceneAsynchronously());

    }

    IEnumerator JoinSceneAsynchronously()
    {
        /*        PhotonNetwork.LeaveRoom();
                PhotonNetwork.ConnectUsingSettings();

                gameController.GetComponent<JoinToRoom>().JoinRoom();*/

        PhotonNetwork.LoadLevel(newSceneName);
        yield return null;

    }
}
