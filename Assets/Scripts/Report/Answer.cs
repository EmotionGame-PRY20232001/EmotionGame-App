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
    protected ThemeColorFilled ThemeRevision;
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

        if (ThemeRevision != null && gm.IsPlayerActive())
            ThemeRevision.OnFillTypeChange(correctEmotion == responseEmotion ?
                                            Theme.ETypes.Accent :
                                            Theme.ETypes.Danger);
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
