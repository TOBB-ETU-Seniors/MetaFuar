using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static string logId;
    public static string usrName;

    private void Start()
    {
        Debug.Log("Logged id:" + logId);
        Debug.Log("Logged username:" + usrName);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(instance);
    }
}
