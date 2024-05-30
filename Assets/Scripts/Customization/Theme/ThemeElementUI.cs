using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ThemeElementUI : ThemeElement
{
    protected Image image; //RawImage

    protected override void Awake()
    {
        image = GetComponent<Image>();
    }

    protected override void UpdateThemeElement()
    {
        if (image == null) return;

        Theme.CustomBackground bgData = GetBackgroundData();
        switch (ElementType)
        {
            case EElement.Background:
                image.sprite = bgData.Texture; //texture
                break;
            case EElement.Frame:
                image.sprite = bgData.Frame;
                break;
            case EElement.Mirror:
                image.sprite = bgData.Mirror;
                break;
            case EElement.SpeechTail:
                image.sprite = bgData.SpeechTail;
                break;
        }
    }
}
