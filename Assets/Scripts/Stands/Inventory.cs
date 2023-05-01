using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Networking;
using System.Linq;
using OVRSimpleJSON;
using Newtonsoft.Json;
using System;

public class Inventory : MonoBehaviour
{
    // find a better way to initialize this
    string backenduri = "https://34.118.241.44:8001";

    // Altindaki urunleri ara(bunlarin sahnedeki isimleri ile veritabanindan cektigimiz isimleri eslestirecegiz)
    // Oradan controller script' lerini al
    // onlarin panel diye objeleri var
    // panel objelerinin altinda item_panel_controller var
    // bu controller altinda da  header, body ve resimler ile ilgili referanslar var
    // zaten buradan ilgili fonksiyonlar ile g�ncelleme yapilabilir

    IEnumerator GetInventory()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(backenduri + "/get_inventory");
        webRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        webRequest.useHttpContinue = false;
        using (webRequest)
        {

            Debug.Log("Get Inventory Called");
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            Debug.Log(webRequest.error);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:


                    GameObject[] urunler = GameObject.FindGameObjectsWithTag("Urun");
                    Debug.Log(webRequest.downloadHandler.text);
                    List<Urun> urunler_info = (List<Urun>)JsonConvert.DeserializeObject(webRequest.downloadHandler.text.Substring(0, webRequest.downloadHandler.text.Length), typeof(List<Urun>));
                    for(int i = 0;i<urunler_info.Count; i++)
                    {
                        var db_name = urunler_info[i].name;
                        var db_desc = urunler_info[i].description;
                        var db_price = urunler_info[i].price;
                        var db_images = urunler_info[i].images;

                        // find index of right object
                        int j = 0;

                        for (; j < urunler.Length; j++)
                        {
                            if (urunler[j].name == db_name)
                                break;
                        }
                        int index = j;

                        var panel_controller = urunler[index].GetComponent<UrunController>().panel.GetComponent<ItemPanelController>();


                        panel_controller.gameObject.SetActive(true);
                        // now set correct values for this object
                        panel_controller.SetTitle(db_name);
                        panel_controller.SetBodyText(db_desc);
                        StartCoroutine(panel_controller.SetImages(db_images.ToArray()));
                        panel_controller.SetPrice(db_price);
                        
                        panel_controller.gameObject.SetActive(false);
                        Debug.Log("An inventory panel has been updated...");
                    }

                    /*
                     * "items": [
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

                    /*
                     * returning response list of objects below
                     {
                        "_id": {
                        "$oid": "6429b9ec8e6ecb19acd1fc98"
                        },
                        "name": "Iphone 11 Pro Max",
                        "description": "Apple' s new creation",
                        "price": "200",
                        "images": [
                        "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MX0H2?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1567304952459",
                        "https://productimages.hepsiburada.net/s/337/375/110000115788449.jpg",
                        "https://www.trustedreviews.com/wp-content/uploads/sites/54/2019/10/iphone11promax-1-1.jpeg",
                        "https://www.apple.com/newsroom/images/tile-images/Apple_iPhone-11-Pro_Most-Powerful-Advanced_091019.jpg.og.jpg?202303301813"
                        ]
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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetInventory());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}