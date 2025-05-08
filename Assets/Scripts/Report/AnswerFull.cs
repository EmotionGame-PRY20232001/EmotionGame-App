using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerFull : Answer
{
    [SerializeField]
    protected TMPro.TMP_Text txtDateTime;
    [SerializeField]
    protected TMPro.TMP_Text txtDuration;
    [SerializeField]
    protected TMPro.TMP_Text txtCorrectEmotion;
    [SerializeField]
    protected TMPro.TMP_Text txtSelectedEmotion;
    [SerializeField]
    protected RectTransform ContextExercise;

    public void LoadTime(System.DateTime completedAt, float secondsToSolve)
    {
        if (txtDateTime != null)
        {
            txtDateTime.text = completedAt.ToShortDateString() + " "
                               + completedAt.ToShortTimeString();
        }

        if (txtDuration != null)
        {
            System.TimeSpan duration = System.TimeSpan.FromSeconds(secondsToSolve);
            txtDuration.text = string.Format("{0:mm\\:ss\\:fff}", duration);
        }
    }

    protected override void LoadEmotion(Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        base.LoadEmotion(correctEmotion, responseEmotion);
        var gm = GameManager.Instance;

        if (txtCorrectEmotion != null)
            txtCorrectEmotion.text = gm.Emotions[correctEmotion].Name;

        if (txtSelectedEmotion != null)
            txtSelectedEmotion.text = gm.Emotions[responseEmotion].Name;
    }

    public override void LoadChoose(Sprite exercisePhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        Photo.enabled = true;
        ContextExercise.gameObject.SetActive(false);
        PlayerImitationPhoto.gameObject.SetActive(false);
        base.LoadChoose(exercisePhoto, correctEmotion, responseEmotion);
    }

    public override void LoadContext(string text, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        Photo.enabled = false;
        ContextExercise.gameObject.SetActive(true);
        PlayerImitationPhoto.gameObject.SetActive(false);
        base.LoadContext(text, correctEmotion, responseEmotion);
    }

    public override void LoadImitate(Sprite exercisePhoto, Sprite playerPhoto, Emotion.EEmotion correctEmotion, Emotion.EEmotion responseEmotion)
    {
        Photo.enabled = true;
        ContextExercise.gameObject.SetActive(false);
        PlayerImitationPhoto.gameObject.SetActive(true);
        base.LoadImitate(exercisePhoto, playerPhoto, correctEmotion, responseEmotion);
    }
}
