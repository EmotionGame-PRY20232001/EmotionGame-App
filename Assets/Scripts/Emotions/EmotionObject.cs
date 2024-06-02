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

    public ImageType ImgType = ImageType.Regular;
    [field:SerializeField]
    public Emotion.EEmotion CurrEmotion { get; protected set; }
    [SerializeField]
    protected bool IsRandom;
    protected Image EmotionImage;
    [SerializeField]
    protected TMP_Text EmotionName;
    protected Vector2 Dir;

    // Glow
    [SerializeField]
    protected GameObject Glow;
    [SerializeField]
    protected float GlowLoopTime = 1.0f;
    [SerializeField][Tooltip("Number of rotations per Scale")]
    protected float GlowLoopRotFactor = 1.0f;

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
        GlowEffect();
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
            EmotionImage.sprite = ImgType == ImageType.Regular ? emoData.SpriteColor : emoData.Icon;
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

    protected void GlowEffect()
    {
        if (Glow == null) return;

        {
            Image imgGlow = Glow.GetComponent<Image>();
            if (imgGlow != null)
            {
                Emotion emoData = GameManager.Instance.Emotions[CurrEmotion];
                imgGlow.color = emoData.Color * 1.5f;
            }
        }

        float rotationZ = Random.Range(0.0f, 360.0f);
        Glow.transform.rotation = Quaternion.AngleAxis(rotationZ, Vector3.forward);

        LeanTween.scale(Glow, Vector3.one, GlowLoopTime)
            .setFrom(Vector3.one * 1.1f)
            .setLoopPingPong()
            .setIgnoreTimeScale(true);
        LeanTween.rotateAroundLocal(Glow, Vector3.forward, 360f, GlowLoopRotFactor)
            .setRepeat(-1)
            .setIgnoreTimeScale(true);
    }
}