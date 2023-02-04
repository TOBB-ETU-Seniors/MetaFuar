using UnityEngine;

public class Scroll : MonoBehaviour
{

    public float scrollSpeed = 50.0f;

    private float offset;

    private RectTransform contentRectTransform;

    void Start()
    {
        contentRectTransform = GetComponent<RectTransform>();
        offset = contentRectTransform.rect.height;
    }

    void Update()
    {
        Vector2 newPosition = contentRectTransform.anchoredPosition;
        newPosition.y += scrollSpeed * Time.deltaTime;

        if (newPosition.y > offset)
        {
            newPosition.y = offset;
        }

        contentRectTransform.anchoredPosition = newPosition;

    }

    private void OnDisable()
    {
        contentRectTransform.anchoredPosition = Vector2.zero;
    }

}
