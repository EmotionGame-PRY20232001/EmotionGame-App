using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class EmotionObject : MonoBehaviour
{
    [field:SerializeField]
    public Exercise.EEmotion CurrEmotion { get; protected set; }
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

        int cant = System.Enum.GetNames(typeof(Exercise.EEmotion)).Length;
        CurrEmotion = (Exercise.EEmotion)Random.Range(0, cant - 1);
        Debug.Log("[EmotionObject] Loading random: " + CurrEmotion);
        LoadByEmotion();
    }

    protected void LoadByEmotion()
    {
        if (GameManager.Instance != null && EmotionImage != null)
        {
            Exercise.Emotion emoData = GameManager.Instance.Emotions[CurrEmotion];
            EmotionImage.sprite = emoData.Sprite;
            if (EmotionName != null)
                EmotionName.text = emoData.Name;
        }
    }

    public void SetEmotion(Exercise.EEmotion emotion)
    {
        CurrEmotion = emotion;
        LoadByEmotion();
    }

}