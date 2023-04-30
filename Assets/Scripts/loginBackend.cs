using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class loginBackend : MonoBehaviour
{
    static string backenduri = "https://34.118.241.44:8001";

    [System.Serializable]
    public class LoginData
    {
        public string login_code;
    }

    public static IEnumerator LogInWithToken(string token)
    {
        LoginData jsonData = new LoginData();
        jsonData.login_code = token;
        
        string jsonStr = JsonUtility.ToJson(jsonData);
        Debug.Log(jsonStr);

        UnityWebRequest request = UnityWebRequest.Post(backenduri+ "/verify_code", jsonStr);

        request.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        request.useHttpContinue = false;

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);

    }
}
