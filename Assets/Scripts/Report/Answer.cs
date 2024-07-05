using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Answer : MonoBehaviour
{
    [SerializeField]
    protected Image Photo;
    [SerializeField]
    protected Image CorrectEmotion;
    [SerializeField]
    protected Image ResponseEmotion;
    [SerializeField]
    protected Image BackgroundRevision;
    [SerializeField]
    protected TMP_Text ContextText;
    [SerializeField]
    protected Image PlayerImitationPhoto;

    protected void LoadEmotion(Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        var gm = GameManager.Instance;

        if (gm == null)
            return;

        if (CorrectEmotion != null)
            CorrectEmotion.sprite = gm.Emotions[correctEmotion].SpriteColor;

        if (ResponseEmotion != null)
            ResponseEmotion.sprite = gm.Emotions[responseEmotion].SpriteColor;

        if (BackgroundRevision != null && gm.IsPlayerActive())
        {
            Theme.ETheme theme = gm.GetBackgrounds()[(Theme.EBackground)gm.currentPlayer.BackgroundId].Theme;

            if (correctEmotion == responseEmotion)
            {
                BackgroundRevision.color = gm.ThemeCustom.Themes[theme].Accent.Main.Background;
            }
            else
            {
                BackgroundRevision.color = gm.ThemeCustom.Themes[theme].Danger.Main.Background;
            }
        }
    }

    public void LoadChoose(Sprite exercisePhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        if (Photo != null)
            Photo.sprite = exercisePhoto;

        LoadEmotion(correctEmotion, responseEmotion);
    }

    public void LoadContext(string text, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        if (ContextText != null)
            ContextText.text = text;

        LoadEmotion(correctEmotion, responseEmotion);
    }

    public void LoadImitate(Sprite exercisePhoto, Sprite playerPhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        if (Photo != null)
            Photo.sprite = exercisePhoto;

        if (PlayerImitationPhoto != null)
            PlayerImitationPhoto.sprite = playerPhoto;

        LoadEmotion(correctEmotion, responseEmotion);
    }
}
