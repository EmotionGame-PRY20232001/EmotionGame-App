using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PopUp))]
public class AnswerPopUp : MonoBehaviour
{
    [field:SerializeField]
    public PopUp PopUp { get; protected set; }
    [SerializeField]
    protected Image Photo;
    [SerializeField]
    private TMP_Text ExerciseText;
    [SerializeField]
    private CharacterExpressions Expressions;
    [SerializeField]
    protected RectTransform BackEffect;
    [SerializeField]
    protected EmotionObject CorrectEmotion;
    [SerializeField][Tooltip("For wrong selected emotion")]
    protected EmotionObject SelectedEmotion;
    [SerializeField]
    protected float EffectTime;
    //public enum EAnswerType { Good, Bad };

    // Response
    [SerializeField]
    protected TMP_Text TextDuration;
    [SerializeField]
    protected TMP_Text CompletedDT;


    protected void Start()
    {
        if (PopUp != null)
        {
            PopUp.onClose += OnPopUpClose;
        }
    }

    protected void LoadPhoto(Sprite photo)
    {
        if (Photo == null) return;
        Photo.sprite = photo;
    }

    protected void LoadExercisePreview(EmotionPhoto emotionEx)
    {
        if (emotionEx.Photo != null)
        {
            Sprite photo = emotionEx.Photo.sprite;
            LoadPhoto(photo);
        }
        else
        {
            EmotionContext emotionCtx = (EmotionContext)emotionEx;
            if (emotionCtx != null)
            {
                if (ExerciseText != null)
                    ExerciseText.text = emotionCtx.Text;
                if (Expressions != null)
                {
                    Expressions.Load();
                    //Popup needs to be active before playing
                    Expressions.PlayEmotion(emotionCtx.PhotoEmotion);
                }
            }
        }

        System.DateTime completedAt = System.DateTime.Now;
        if (TextDuration != null)
        {
            float duration = (float)(completedAt - emotionEx.StartedAt).TotalSeconds;
            duration = Mathf.Round(duration * 100) / 100;
            TextDuration.text = "¡" + duration.ToString() + " segundos!";
        }
        if (CompletedDT != null)
            CompletedDT.text = completedAt.ToString();
    }

    public void LoadAnswerCorrect(EmotionPhoto emotionEx)
    {
        LoadExercisePreview(emotionEx);

        if (CorrectEmotion != null)
            CorrectEmotion.SetEmotion(emotionEx.PhotoEmotion);

        if (BackEffect != null)
            LeanTween.rotateAroundLocal(BackEffect, Vector3.forward, 360f, EffectTime)
                .setRepeat(-1)
                .setIgnoreTimeScale(true);
    }

    public void LoadAnswerWrong(EmotionPhoto emotionEx, Emotion.EEmotion emotionSelected)
    {
        LoadExercisePreview(emotionEx);

        if (CorrectEmotion != null)
            CorrectEmotion.SetEmotion(emotionEx.PhotoEmotion);
        if (SelectedEmotion != null)
            SelectedEmotion.SetEmotion(emotionSelected);

        float start = BackEffect.rect.height / 2;
        start -= BackEffect.rect.width / 4;
        start += 48.0f;
        if (BackEffect != null)
            LeanTween.moveY(BackEffect, start, EffectTime)
                .setFrom(start + 160.0f)
                .setLoopPingPong()
                .setIgnoreTimeScale(true);
    }

    public void OnPopUpClose()
    {
        LeanTween.cancel(BackEffect);
    }
}
