using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class EmotionObject : MonoBehaviour
{
    public enum ImageType { Regular, Icon }

    public bool ShowText = true;
    public ImageType ImgType = ImageType.Regular;
    [field:SerializeField]
    public Emotion.EEmotion CurrEmotion { get; protected set; }

    [SerializeField]
    protected bool IsRandom;
    [SerializeField]
    protected TMP_Text EmotionName;
    [SerializeField]
    protected Image EmotionColor;
    [SerializeField]
    protected Image Glow;

    protected Image EmotionImage;
    protected Vector2 Dir;

    protected virtual void Awake()
    {
        EmotionImage = gameObject.GetComponent<Image>();
        if (!ShowText && EmotionName != null)
            EmotionName.enabled = false;
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
        if (GameManager.Instance == null) return;
        Emotion emoData = GameManager.Instance.Emotions[CurrEmotion];

        if (EmotionImage != null)
            EmotionImage.sprite = ImgType == ImageType.Regular ? emoData.SpriteColor : emoData.Icon;

        if (EmotionName != null)
            EmotionName.text = emoData.Name;

        if (Glow != null)
            Glow.color = emoData.Color * 1.5f;

        if (EmotionColor != null)
            EmotionColor.color = emoData.Color;
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