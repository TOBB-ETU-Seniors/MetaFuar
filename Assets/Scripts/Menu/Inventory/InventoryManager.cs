using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    string backenduri = "https://34.118.241.44:8002";

    public List<Product> products;
    public List<Product> selectedProducts;

    public TMP_Text userBalanceInfo;

    private void OnEnable()
    {
        StartCoroutine(_GetProductsInInventory());
    }

    public void AddProduct(Product product)
    {
        products.Add(product);

        GameObject content = GetComponentInChildren<ScrollRect>().content.gameObject;

        GameObject togglePrefab = GameObject.Find("BaseToggle");

        GameObject newToggle = Instantiate(togglePrefab, content.transform);

        newToggle.SetActive(true);

        //GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 0f;

    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
    }

    public void AddSelectedProduct(Product product)
    {
        selectedProducts.Add(product);
    }

    public void RemoveSelectedProduct(Product product)
    {
        selectedProducts.Remove(product);
    }

    public void DeleteSelectedProducts()
    {
        foreach (Product product in selectedProducts)
        {
            //Delete(product.product_id);
            Debug.Log("Silindi: " + product.product_name);

        }

        selectedProducts.Clear();
    }



    IEnumerator _GetProductsInInventory()
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

                    userBalanceInfo.text = userInfo.user_balance.ToString();

                    foreach (Urun urun in urunler_info)
                    {
                        Product product = new Product();
                        product.product_id = urun._id;
                        product.product_name = urun.name;
                        product.price = urun.price;
                        product.SetImage(urun.images[0]);

                        AddProduct(product);
                    }

                    Debug.Log(webRequest.downloadHandler);

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Could not connect to the server");

                    break;
            }

        }
    }

}
