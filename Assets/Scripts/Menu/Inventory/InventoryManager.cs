using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    string backenduri = "https://34.118.241.44:8001";

    public List<GameObject> productToggles;
    public List<GameObject> selectedProductToggles;

    public TMP_Text userBalanceInfo;

    public GameObject baseToggle;

    [HideInInspector]
    public bool panelUpToDate = false;

    private void OnEnable()
    {
        if (!panelUpToDate)
            StartCoroutine(_GetProductsInInventory());
    }

    public void AddProduct(Product product)
    {
        foreach (GameObject toggle in productToggles)
        {
            if (toggle.GetComponent<Product>().product_id == product.product_id)
            {
                Debug.Log("Ürün daha önce eklendi");
                return;
            }
        }

        GameObject content = GetComponentInChildren<ScrollRect>().content.gameObject;

        GameObject newToggle = Instantiate(baseToggle, content.transform);

        Product toggleProduct = newToggle.GetComponent<Product>();
        toggleProduct.product_id = product.product_id;
        toggleProduct.product_name = product.product_name;
        toggleProduct.product_price = product.product_price;
        toggleProduct.image_link = product.image_link;

        toggleProduct.SetProductInfoText();
        StartCoroutine(toggleProduct.SetImage());

        newToggle.SetActive(true);

        productToggles.Add(newToggle);

    }

    public void RemoveProduct(GameObject productToggle)
    {
        productToggles.Remove(productToggle);
    }

    public void AddSelectedProduct(GameObject productToggle)
    {
        selectedProductToggles.Add(productToggle);
    }

    public void RemoveSelectedProduct(GameObject productToggle)
    {
        selectedProductToggles.Remove(productToggle);
    }

    public void DeleteSelectedProducts()
    {
        foreach (GameObject productToggle in selectedProductToggles)
        {
            Product toggleProduct = productToggle.GetComponent<Product>();
            StartCoroutine(_DeleteProductInInventory("63e4ae12f9bdcd50c8e582d6", toggleProduct.product_id));
            productToggles.Remove(productToggle);
            Debug.Log("Silindi: " + toggleProduct.product_name);
        }

        selectedProductToggles.Clear();

        panelUpToDate = false;
        StartCoroutine(_GetProductsInInventory());
    }



    public IEnumerator _GetProductsInInventory()
    {
        if (!panelUpToDate)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(backenduri + "/get_inventory?user_id=" + "63e4ae12f9bdcd50c8e582d6");
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
                        //UserInfo userInfo = (UserInfo)JsonConvert.DeserializeObject(webRequest.downloadHandler.text.Substring(0, webRequest.downloadHandler.text.Length), typeof(UserInfo));
                        UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(webRequest.downloadHandler.text);

                        List<Urun> urunler_info = userInfo.items;
                        // find index of right object

                        userBalanceInfo.text = userInfo.user_balance.ToString() + " $";

                        foreach (Urun urun in urunler_info)
                        {
                            Product product = new Product();
                            product.product_id = urun._id;
                            product.product_name = urun.name;
                            product.product_price = urun.price;
                            product.image_link = urun.images[0];

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
        panelUpToDate = true;
    }



    public IEnumerator _DeleteProductInInventory(string userId, string productId)
    {
        IDictionary<string, string> data = new Dictionary<string, string>();
        data.Add("user", userId);
        data.Add("base_item", productId);

        UnityWebRequest webRequest = UnityWebRequest.Post(backenduri + "/remove_item_users_inventory", JsonConvert.SerializeObject(data));
        webRequest.certificateHandler = new AcceptAllCertificatesSignedWithASpecificKeyPublicKey();
        webRequest.useHttpContinue = false;
        using (webRequest)
        {
            Debug.Log("remove_item_users_inventory Called");
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();


            Debug.Log(webRequest.error);
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    Debug.Log("Product removed: " + productId);

                    Debug.Log(webRequest.downloadHandler);

                    break;
                case UnityWebRequest.Result.ConnectionError:
                    Debug.Log("Could not connect to the server");

                    break;
            }

        }
    }

}
