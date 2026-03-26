using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ResizeImage();
    }

    void Update()
    {
        if (rectTransform.sizeDelta.x != Screen.width || rectTransform.sizeDelta.y != Screen.height)
        {
            ResizeImage();
        }
    }

    void ResizeImage()
    {
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}