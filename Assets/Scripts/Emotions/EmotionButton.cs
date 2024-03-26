using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class EmotionButton : MonoBehaviour
{
    public Emotion.EEmotion CurrEmotion;
    [SerializeField]
    protected bool IsRandom;
    protected Button Button;
    protected Image EmotionImage;
    
    protected void Awake()
    {
        Button = gameObject.GetComponent<Button>();
        EmotionImage = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        LoadRandom();
        LoadImageByEmotion();
    }

    protected void LoadRandom()
    {
        if (IsRandom)
        {
            int cant = System.Enum.GetNames(typeof(Emotion.EEmotion)).Length;
            CurrEmotion = (Emotion.EEmotion)Random.Range(0, cant - 1);
            Debug.Log(CurrEmotion);
        }
    }

    protected void LoadImageByEmotion()
    {
        if (GameManager.Instance != null && EmotionImage != null)
        {
            EmotionImage.sprite = GameManager.Instance.Emotions[CurrEmotion].Sprite;
        }
    }

    public void PressButton()
    {
        if (ActivityManager.Instance != null)
        {
            if (CurrEmotion == ActivityManager.Instance.ExerciseEmotion) ActivityManager.Instance.Good();
            else ActivityManager.Instance.Bad();
        }
    }

}