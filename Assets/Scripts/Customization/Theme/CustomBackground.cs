using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CustomBackground : ThemeElement
{
    RawImage image;

    protected void Awake()
    {
        image = GetComponent<RawImage>();
    }

    protected override void UpdateThemeElement()
    {
        if (image != null)
            image.texture = GameManager.Instance.GetBackgrounds()[Id].Texture;
    }
}
