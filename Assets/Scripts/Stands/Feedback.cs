using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class Feedback : MonoBehaviourPun
{
    public Canvas canvas;

    private Graphic graphic;
    private Color baseColor;

    private void Start()
    {
        graphic = canvas.GetComponent<Graphic>();
        baseColor = graphic.color;
    }

    private void LightGreen()
    {
        Color c = new Color(0, 1, 0,1);
        Debug.Log("Greeen!!");
        graphic.color = c;
    }

    private void LightRed()
    {
        Color c = new Color(1, 0, 0, 1);
        Debug.Log("RReeeD!!");
        graphic.color = c;
    }

    public void LightAsDefault()
    {
        graphic.color = baseColor;
    }

    public void UpFeedBack()
    {
        Debug.Log("Up");
        if (photonView.IsMine)
        {
            Debug.Log("Up2");
            photonView.RPC("UpFeedbackPhoton", RpcTarget.AllBuffered, null);
        }
    }

    public void DownFeedBack()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("DownFeedbackPhoton", RpcTarget.AllBuffered, null);
        }
    }

    [PunRPC]
    public void UpFeedbackPhoton()
    {

        StartCoroutine(UpFeedBackNumerator());

    }

    [PunRPC]
    public void DownFeedbackPhoton()
    {
        
        StartCoroutine(DownFeedBackNumerator());
    }

    private System.Collections.IEnumerator UpFeedBackNumerator()
    {
        LightGreen();
        yield return new WaitForSeconds(1f);
        LightAsDefault();

    }

    private System.Collections.IEnumerator DownFeedBackNumerator()
    {
        LightRed();
        yield return new WaitForSeconds(1f);
        LightAsDefault();

    }
}
