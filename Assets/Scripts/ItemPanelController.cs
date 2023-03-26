using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text bodyText;

    [SerializeField] private RawImage[] images;

    public void SetTitle(string text)
    {
        title.text = text;
    }

    public void SetBodyText(string text)
    {
        bodyText.text = text;
    }

    public void SetImages(Image[] images)
    {
        throw new System.NotImplementedException();
    }

    public void PurchaseItem(int itemCode)
    {
        throw new System.NotImplementedException();
    }

}
