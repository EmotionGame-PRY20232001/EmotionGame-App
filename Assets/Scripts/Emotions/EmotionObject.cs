using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class EmotionObject : MonoBehaviour
{
    [field:SerializeField]
    public Emotion.EEmotion CurrEmotion { get; protected set; }
    [SerializeField]
    protected bool IsRandom;
    protected Image EmotionImage;
    [SerializeField]
    protected TMP_Text EmotionName;

    protected virtual void Awake()
    {
        EmotionImage = gameObject.GetComponent<Image>();
    }

    protected virtual void Start()
    {
        if (IsRandom)
            LoadRandom();
        else
            LoadByEmotion();
    }

    protected void LoadRandom()
    {
        if (!IsRandom) return;

        int cant = System.Enum.GetNames(typeof(Emotion.EEmotion)).Length;
        CurrEmotion = (Emotion.EEmotion)Random.Range(0, cant - 1);
        Debug.Log("[EmotionObject] Loading random: " + CurrEmotion);
        LoadByEmotion();
    }

    protected void LoadByEmotion()
    {
        if (GameManager.Instance != null && EmotionImage != null)
        {
            Emotion.Data emoData = GameManager.Instance.Emotions[CurrEmotion];
            EmotionImage.sprite = emoData.Sprite;
            if (EmotionName != null)
                EmotionName.text = emoData.Name;
        }
    }

    public void SetEmotion(Emotion.EEmotion emotion)
    {
        CurrEmotion = emotion;
        LoadByEmotion();
    }

}