using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ItemPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text bodyText;

    [SerializeField] private RawImage[] images;
    [SerializeField] private int price;

    public void SetPrice(int price)
    {
        this.price = price;
    }
    public void SetTitle(string text)
    {
        title.text = text;
    }

    public void SetBodyText(string text)
    {
        bodyText.text = text;
    }

    public IEnumerator SetImages(string[] image_links)
    {

        Debug.Log("I have recieved following image links: ");
        Debug.Log(image_links);
        for(int i = 0; i< image_links.Length; i++) 
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(image_links[i]);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else {  
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                images[i].GetComponent<RawImage>().texture = myTexture;
                Debug.Log("I updated image textures");
            }
        }
    }

    public void PurchaseItem(int itemCode)
    {
        throw new System.NotImplementedException();
    }

}
