using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Product : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string product_id;
    public string product_name;
    public int price;

    [SerializeField] private RawImage rawImage;

    [SerializeField] private GameObject productInfo;

    [SerializeField] private InventoryManager inventoryManager;

    private Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnProductToggleValueChanged);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnProductToggleValueChanged);
    }

    private void OnEnable()
    {
        productInfo.GetComponentInChildren<TMP_Text>().text = "isim\nfiyat";
    }

    public void SetPrice(int price)
    {
        this.price = price;
    }

    public IEnumerator SetImage(string image_link)
    {
        Debug.Log("I have received the following image links:");
        Debug.Log(image_link);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(image_link);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rawImage.texture = myTexture;
            Debug.Log("I updated image texture");
        }
    }

    public void OnProductToggleValueChanged(bool value)
    {
        if (value)
        {
            inventoryManager.AddSelectedProduct(this);
        }
        else
        {
            inventoryManager.RemoveSelectedProduct(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter == toggle.gameObject) // Sadece toggle üzerindeyken iþlem yaparýz
        {
            if (OnPointerEnterEvent != null)
            {
                OnPointerEnterEvent.Invoke();
            }
        }

        productInfo.SetActive(true);
    }

    public event System.Action OnPointerEnterEvent;

    public void OnPointerExit(PointerEventData eventData)
    {
        productInfo.SetActive(false);
    }

}