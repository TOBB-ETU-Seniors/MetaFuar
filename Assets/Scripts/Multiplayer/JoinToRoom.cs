using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class JoinToRoom : MonoBehaviourPunCallbacks
{
    public void JoinRoom()
    {
        Debug.Log("Joining the room");
        PhotonNetwork.JoinRoom("Room1");        
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room has not been created yet!");
        Debug.Log(message);
        Debug.Log("Creating room");
        PhotonNetwork.CreateRoom("Room1");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        PhotonNetwork.LoadLevel("Movement&ControlsScene");
    }

}
