using UnityEngine;
using UnityEngine.UI;

public class SetImage : MonoBehaviour
{
    Image image;
    GameObject parent;
    void Start()
    {
        image = GetComponent<Image>();
        parent = transform.parent.gameObject;
        image.sprite = parent.GetComponent<Image>().sprite;
        image.SetNativeSize();
    }
}
