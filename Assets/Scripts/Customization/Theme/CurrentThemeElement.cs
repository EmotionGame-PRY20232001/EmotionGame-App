using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CurrentThemeElement : ThemeElement
{
    public enum EElement { Frame, SpeechTail, Mirror };

    /// 
    protected Image CurrentImage;
    [SerializeField]
    protected EElement ElementType;

    protected void Awake()
    {
        UseFromPlayer = true;
        CurrentImage = GetComponent<Image>();
    }

    protected override void UpdateThemeElement()
    {
        if (CurrentImage == null) return;

        switch(ElementType)
        {
            case EElement.Frame:
                CurrentImage.sprite = GameManager.Instance.GetBackgrounds()[Id].Frame;
                break;
            case EElement.Mirror:
                CurrentImage.sprite = GameManager.Instance.GetBackgrounds()[Id].Mirror;
                break;
            case EElement.SpeechTail:
                CurrentImage.sprite = GameManager.Instance.GetBackgrounds()[Id].SpeechTail;
                break;
        }
    } 
}
