using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;


public class login : MonoBehaviour
{
    void Awake ()
    {
        if (!FB.IsInitialized) {
            FB.Init(InitCallback, OnHideUnity);
        } else {
            FB.ActivateApp();
        }
    }

    private void InitCallback ()
    {
        if (FB.IsInitialized) {
            FB.ActivateApp();
            Debug.Log("Initialized the Facebook SDK");

        } else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity (bool isGameShown)
    {
        if (!isGameShown) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }


    public void Login(){
        var perms = new List<string>(){"public_profile", "email"};
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback (ILoginResult result) {

        if (result.Error != null){
            Debug.Log(result.Error);
        }


        if (FB.IsLoggedIn) {            
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;    
            Debug.Log(aToken.UserId);
            
            foreach (string perm in aToken.Permissions) {
                Debug.Log(perm);
            }
            Debug.Log("User Logged In");
        } else {
            Debug.Log("User cancelled login");
        }
    }

}
