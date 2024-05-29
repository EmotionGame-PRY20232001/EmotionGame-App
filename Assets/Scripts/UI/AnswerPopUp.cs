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
    protected GameObject BackEffect;
    protected Vector3 BackEffectPosition = Vector3.zero;
    [SerializeField]
    protected EmotionObject CorrectEmotion;
    [SerializeField]
    protected EmotionObject SelectedEmotion; // for wrong selected emotion
    [SerializeField]
    protected float EffectTime;
    //public enum EAnswerType { Good, Bad };

    protected void Start()
    {
        if (PopUp != null)
        {
            PopUp.onClose += OnPopUpClose;
        }
        BackEffectPosition = Vector3.zero;
        LoadBackEffectPosition();
    }

    protected void LoadBackEffectPosition()
    {
        if (BackEffectPosition == Vector3.zero && BackEffect != null)
        {
            RectTransform rt = BackEffect.GetComponent<RectTransform>();
            if (rt != null)
                BackEffectPosition = rt.position;
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

    public void LoadAnswerWrong(EmotionPhoto emotionEx, Exercise.EEmotion emotionSelected)
    {
        LoadExercisePreview(emotionEx);

        if (CorrectEmotion != null)
            CorrectEmotion.SetEmotion(emotionEx.PhotoEmotion);
        if (SelectedEmotion != null)
            SelectedEmotion.SetEmotion(emotionSelected);

        LoadBackEffectPosition();
        const float start = 48.0f;
        const float end = start + 160.0f;
        if (BackEffect != null)
            LeanTween.moveY(BackEffect, BackEffectPosition.y + start, EffectTime)
                .setFrom(BackEffectPosition.y + end)
                .setLoopPingPong()
                .setIgnoreTimeScale(true);
    }

    public void OnPopUpClose()
    {
        LeanTween.cancel(BackEffect);
    }
}
