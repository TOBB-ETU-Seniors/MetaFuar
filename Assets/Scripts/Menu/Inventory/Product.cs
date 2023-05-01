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
    public int product_price;
    public string image_link;

    [SerializeField] private RawImage rawImage;

    [SerializeField] private GameObject productInfo;

    [SerializeField] private InventoryManager inventoryManager;

    private Toggle toggle;

    private bool imageSet = false;

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
        if (!imageSet)
            StartCoroutine(SetImage());
    }

    public void SetProductInfoText()
    {
        productInfo.GetComponentInChildren<TMP_Text>().text = product_name + "\n" + product_price + " $";
    }

    public IEnumerator SetImage()
    {
        Debug.Log("I have received the following image links:");
        Debug.Log(image_link);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(image_link);
        yield return www.SendWebRequest();

        Debug.Log("SendWebRequest");

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Texture get");
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            rawImage.texture = myTexture;
            Debug.Log("I updated image texture");
        }

        imageSet = true;
    }

    public void OnProductToggleValueChanged(bool value)
    {
        if (value)
        {
            inventoryManager.AddSelectedProduct(this.gameObject);
        }
        else
        {
            inventoryManager.RemoveSelectedProduct(this.gameObject);
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