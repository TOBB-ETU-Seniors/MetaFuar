using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{    

    public static GameController instance;

    private void Start(){
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(instance);       
    }
}