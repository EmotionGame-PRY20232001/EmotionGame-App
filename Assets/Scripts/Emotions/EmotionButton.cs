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
    protected bool Random;
    [SerializeField] 
    protected int Velocity;
    protected Button Button;
    protected Image EmotionImage;
    protected RectTransform RectTransform;
    
    protected void Awake()
    {
        Button = gameObject.GetComponent<Button>();
        EmotionImage = gameObject.GetComponent<Image>();
        RectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        LoadRandom();
        LoadImageByEmotion();
    }

    private void Update()
    {
        var anchoredPos = RectTransform.anchoredPosition;
        anchoredPos += Vector2.up * Time.deltaTime * Velocity;
        if (anchoredPos.y > 0) Velocity *= -1;
        RectTransform.anchoredPosition = anchoredPos;
    }

    protected void LoadRandom()
    {
        if (Random)
        {
            System.Random ran = new System.Random();
            int cant = System.Enum.GetNames(typeof(Emotion.EEmotion)).Length;
            CurrEmotion = (Emotion.EEmotion)ran.Next(0, cant-1);
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

}