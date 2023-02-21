using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ToggleTextUpdate : MonoBehaviour, IPointerEnterHandler
{
    public string newHeaderText;
    public string newBodyText;

    private ScrollRect scrollRect;
    private GameObject Text { get; set; }
    private TMP_Text headerText;
    private TMP_Text bodyText;

    public void Awake()
    {
        scrollRect = GetComponentInParent<ScrollRect>();
        Text = scrollRect.transform.parent.Find("Text").gameObject;
        headerText = Text.transform.Find("Header").GetComponent<TMP_Text>();
        bodyText = Text.transform.Find("Body").GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        headerText.text = newHeaderText;
        bodyText.text = newBodyText;
        scrollRect.OnDrag(eventData);
    }
}
