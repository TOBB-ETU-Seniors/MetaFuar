using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{

    string root_uri = "http://localhost:8000/";
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(isUsernameAvailable("test entry"));
        StartCoroutine(addUser());
      


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator isUsernameAvailable(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_name", username);
        using (UnityWebRequest www = UnityWebRequest.Post(root_uri+ "isUsernameAvailable/", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("Form upload complete!");
            }
        }
    }
    // root_uri/account
    IEnumerator addUser()
    {

        WWWForm form = new WWWForm();
        form.AddField("login_type", "google");
        form.AddField("login_id", "123123123123");
        form.AddField("user_name", "Alpfischer_test1");
        form.AddField("email_address", "alper_test1@gmail.com");

        using (UnityWebRequest www = UnityWebRequest.Post(root_uri+"account", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("Form upload complete!");
            }
        }
    }

}
