using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;


public class loginBackend : MonoBehaviour
{
    static string backenduri = "https://34.118.241.44:8001";


    public delegate void LoginCallback(LogInfo info);

    public class LogInfo
    {

        public string user_id { get; set; }
        public string user_name { get; set; }

    }



    public static IEnumerator LogInWithToken(string token, LoginCallback callback)
    {

        UnityWebRequest request = UnityWebRequest.Get(backenduri + "/verify_code?login_code=" + token);
        Debug.Log("Sended:" + backenduri + "/verify_code?login_code=" + token);

        request.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        request.useHttpContinue = false;

        yield return request.SendWebRequest();


        Debug.Log("Logined to id:" + request.downloadHandler.text);
        LogInfo info = JsonConvert.DeserializeObject<LogInfo>(request.downloadHandler.text);
        callback(info);
    }
}