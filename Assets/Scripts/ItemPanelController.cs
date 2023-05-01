using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ItemPanelController : MonoBehaviour
{
    string backenduri = "https://34.118.241.44:8002";

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
        for (int i = 0; i < image_links.Length; i++)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(image_links[i]);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
                Debug.Log(www.error);
            else
            {
                Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                images[i].GetComponent<RawImage>().texture = myTexture;
                Debug.Log("I updated image textures");
            }
        }
    }

    public void OnPurchaseItemButtonClicked(string productName)
    {
        StartCoroutine(_PurchaseItemControl(productName));
    }

    IEnumerator _PurchaseItemControl(string productName)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(backenduri + "/get_inventory");
        webRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        webRequest.useHttpContinue = false;
        using (webRequest)
        {
            Debug.Log("get_inventory Called");
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            Debug.Log(webRequest.error);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:


                    Debug.Log(webRequest.downloadHandler.text);
                    UserInfo userInfo = (UserInfo)JsonConvert.DeserializeObject(webRequest.downloadHandler.text.Substring(0, webRequest.downloadHandler.text.Length), typeof(UserInfo));
                    List<Urun> urunler_info = userInfo.items;
                    // find index of right object

                    int i = 0;
                    for (; i < urunler_info.Count; i++)
                    {
                        if (productName == urunler_info[i].name)
                            break;
                    }

                    var db_id = urunler_info[i]._id;
                    var db_name = urunler_info[i].name;
                    var db_price = urunler_info[i].price;

                    if (userInfo.user_balance >= price)
                    {
                        StartCoroutine(_PurchaseItem("63e4ae12f9bdcd50c8e582d6", db_id, db_price));
                    }


                    /*
                     * returning response list of objects below
                     {
                        "items": [
                            {
                                "_id": {
                                    "$oid": "6429b9ec8e6ecb19acd1fc98"
                                },
                                "name": "iphone_14_pro_max",
                                "description": "Apple' s new creation",
                                "price": 200,
                                "images": [
                                    "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MX0H2?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1567304952459",
                                    "https://productimages.hepsiburada.net/s/337/375/110000115788449.jpg",
                                    "https://www.trustedreviews.com/wp-content/uploads/sites/54/2019/10/iphone11promax-1-1.jpeg",
                                    "https://www.apple.com/newsroom/images/tile-images/Apple_iPhone-11-Pro_Most-Powerful-Advanced_091019.jpg.og.jpg?202303301813"
                                ]
                            },
                            {
                                "_id": {
                                    "$oid": "6429b9ec8e6ecb19acd1fc98"
                                },
                                "name": "iphone_14_pro_max",
                                "description": "Apple' s new creation",
                                "price": 200,
                                "images": [
                                    "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MX0H2?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1567304952459",
                                    "https://productimages.hepsiburada.net/s/337/375/110000115788449.jpg",
                                    "https://www.trustedreviews.com/wp-content/uploads/sites/54/2019/10/iphone11promax-1-1.jpeg",
                                    "https://www.apple.com/newsroom/images/tile-images/Apple_iPhone-11-Pro_Most-Powerful-Advanced_091019.jpg.og.jpg?202303301813"
                                ]
                            }
                        ],
                        "user_balance": 1010
                    }

                     */
                    //string[] images = new string[4];
                    //var stands = GameObject.FindGameObjectsWithTag("stand");
                    //foreach (var st in stands)
                    //{
                    //    st.GetComponent<ItemPanelController>().SetImages(images);
                    //}
                    //break;


                    Debug.Log(webRequest.downloadHandler);

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Could not connect to the server");

                    break;
            }

        }
    }

    IEnumerator _PurchaseItem(string userId, string productId, int price)
    {
        IDictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", userId);
        data.Add("base_item", productId);



        UnityWebRequest webRequest = UnityWebRequest.Post(backenduri + "/add_to_users_inventory", JsonConvert.SerializeObject(data));
        webRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        webRequest.useHttpContinue = false;
        using (webRequest)
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            Debug.Log(webRequest.error);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("Product purchased " + productId);

                    StartCoroutine(_DecreaseUserBalance(userId, price));

                    Debug.Log(webRequest.downloadHandler);

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Could not connect to the server");

                    break;
            }

        }
    }

    IEnumerator _DecreaseUserBalance(string userId, int price)
    {
        IDictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", userId);
        data.Add("change_amount", (-price).ToString());

        UnityWebRequest webRequest = UnityWebRequest.Post(backenduri + "/update_user_balance", JsonConvert.SerializeObject(data));
        webRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        webRequest.useHttpContinue = false;
        using (webRequest)
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            Debug.Log(webRequest.error);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("User balance decreased. Price: " + price);

                    Debug.Log(webRequest.downloadHandler);

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Could not connect to the server");

                    break;
            }

        }
    }
}
