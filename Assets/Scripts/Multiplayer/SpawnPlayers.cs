using UnityEngine;
using Photon.Pun;


public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[JoinToRoom.spawnIndex].position, Quaternion.identity);
    }
}
