using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CustomBackground : ThemeElement
{
    Image image; //RawImage

    protected void Awake()
    {
        image = GetComponent<Image>();
    }

    protected override void UpdateThemeElement()
    {
        if (image != null)
            image.sprite = GameManager.Instance.GetBackgrounds()[Id].Texture; //texture
    }
}
