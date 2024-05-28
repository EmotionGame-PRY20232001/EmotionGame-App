using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PopUp))]
public class AnswerPopUp : MonoBehaviour
{
    [field:SerializeField]
    public PopUp PopUp { get; protected set; }
    [SerializeField]
    protected Image Photo;
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

    public void LoadBackEffectPosition()
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
        if (photo == null || Photo == null) return;
        Photo.sprite = photo;
    }

    public void LoadAnswerCorrect(Sprite photo, Exercise.EEmotion emotion)
    {
        LoadPhoto(photo);

        if (CorrectEmotion != null)
            CorrectEmotion.SetEmotion(emotion);

        if (BackEffect != null)
            LeanTween.rotateAroundLocal(BackEffect, Vector3.forward, 360f, EffectTime)
                .setRepeat(-1)
                .setIgnoreTimeScale(true);
    }

    public void LoadAnswerWrong(Sprite photo, Exercise.EEmotion emotionCorrect, Exercise.EEmotion emotionSelected)
    {
        LoadPhoto(photo);

        if (CorrectEmotion != null)
            CorrectEmotion.SetEmotion(emotionCorrect);
        if (SelectedEmotion != null)
            SelectedEmotion.SetEmotion(emotionSelected);

        LoadBackEffectPosition();
        const float start = 48.0f;
        const float end = start + 128.0f;
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
