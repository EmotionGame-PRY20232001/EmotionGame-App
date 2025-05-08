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
    protected CharacterExpressions ContextExpression;
    [SerializeField]
    protected Image PlayerImitationPhoto;
    [field:SerializeField]
    public Button BtnOpenFull { get; protected set; }

    private void Awake()
    {
        BtnOpenFull = gameObject.GetComponent<Button>();
    }

    protected virtual void LoadEmotion(Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        var gm = GameManager.Instance;

        if (gm == null)
            return;

        if (CorrectEmotion != null)
            CorrectEmotion.sprite = gm.Emotions[correctEmotion].Icon;

        if (ResponseEmotion != null)
            ResponseEmotion.sprite = gm.Emotions[responseEmotion].Icon;

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

    public virtual void LoadChoose(Sprite exercisePhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        if (Photo != null)
            Photo.sprite = exercisePhoto;

        LoadEmotion(correctEmotion, responseEmotion);
    }

    public virtual void LoadContext(string text, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        if (ContextText != null)
            ContextText.text = text;

        if (ContextExpression != null)
            ContextExpression.PlayEmotion(correctEmotion);

        LoadEmotion(correctEmotion, responseEmotion);
    }

    public virtual void LoadImitate(Sprite exercisePhoto, Sprite playerPhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        if (Photo != null)
            Photo.sprite = exercisePhoto;

        if (PlayerImitationPhoto != null)
            PlayerImitationPhoto.sprite = playerPhoto;

        LoadEmotion(correctEmotion, responseEmotion);
    }
}
