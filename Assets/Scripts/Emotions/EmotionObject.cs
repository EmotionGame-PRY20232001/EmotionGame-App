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
    public Emotion.EEmotion CurrEmotion { get; protected set; }
    [SerializeField]
    protected bool IsRandom;
    protected Image EmotionImage;
    [SerializeField]
    protected TMP_Text EmotionName;
    protected Vector2 Dir;

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
            Emotion emoData = GameManager.Instance.Emotions[CurrEmotion];
            EmotionImage.sprite = emoData.SpriteColor;
            if (EmotionName != null)
                EmotionName.text = emoData.Name;
        }
    }

    public void SetEmotion(Emotion.EEmotion emotion, int num = -1)
    {
        CurrEmotion = emotion;
        LoadByEmotion();

        if (num == -1) return;
        switch (num)
        {
            case 0: Dir = new Vector2(Random.Range(0.1f, 1.0f), Random.Range(0.1f, 1.0f)).normalized; break;
            case 1: Dir = new Vector2(Random.Range(0.1f, 1.0f), Random.Range(-1.0f, -0.1f)).normalized; break;
            case 2: Dir = new Vector2(Random.Range(-1.0f, -0.1f), Random.Range(-1.0f, -0.1f)).normalized; break;
            case 3: Dir = new Vector2(Random.Range(-1.0f, -0.1f), Random.Range(0.1f, 1.0f)).normalized; break;
            default: break;
        }
        //Dir = Random.insideUnitCircle.normalized;
    }

}