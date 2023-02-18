using UnityEngine;
using Photon.Pun;


public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, this.transform.position, Quaternion.identity);
    }
}
