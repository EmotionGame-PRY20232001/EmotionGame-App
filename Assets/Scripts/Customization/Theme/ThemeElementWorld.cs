using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ThemeElementWorld : ThemeElement
{
    protected SpriteRenderer spriteRenderer; //RawImage

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void UpdateThemeElement()
    {
        if (spriteRenderer == null) return;
        Theme.CustomBackground bgData = GetBackgroundData();

        switch (ElementType)
        {
            case EElement.Background:
                //texture if was rawimage
                spriteRenderer.sprite = bgData.Texture;
                break;
            case EElement.Frame:
                spriteRenderer.sprite = bgData.Frame;
                break;
            case EElement.Mirror:
                spriteRenderer.sprite = bgData.Mirror;
                break;
            case EElement.SpeechTail:
                spriteRenderer.sprite = bgData.SpeechTail;
                break;
        }
    }
}
