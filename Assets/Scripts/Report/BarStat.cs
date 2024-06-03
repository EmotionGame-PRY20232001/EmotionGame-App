using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DisallowMultipleComponent]
public class BarStat : MonoBehaviour
{
    public float Absolute { get; protected set; }
    public float Percentage { get; protected set; }
    protected float DefaultGrow = 192.0f;
    protected float TransitionTime = 0.1f;
    [Tooltip("Else is Horizontal")]
    public bool IsVertical = true;
    [SerializeField]
    protected RectTransform Bar;
    [SerializeField]
    protected TMP_Text AbsoluteText;
    [SerializeField]
    protected TMP_Text PercentText;
    [SerializeField]
    protected EmotionObject EmotionObj;
    [field:SerializeField]
    public Emotion.EEmotion CurrEmotion { get; protected set; }

    protected void Start()
    {
        FillColor();
        ResetPercentage();
    }

    protected void FillColor()
    {
        var gm = GameManager.Instance;
        if (gm == null || Bar == null) return;
        Image img = Bar.GetComponent<Image>();

        if (img == null) return;
        if (EmotionObj != null)
            CurrEmotion = EmotionObj.CurrEmotion;
        img.color = gm.Emotions[CurrEmotion].Color;
    }

    public void ResetPercentage()
    {
        LoadPercentage(0, 0, true);
    }

    public void LoadPercentage(float total, float absoluteVal, bool instant = false)
    {
        if (Bar == null) return;

        float newPercentage = total == 0 ? 0 : absoluteVal / total;
        if (instant)
        {
            SetAbsolute(absoluteVal);
            SetPercentage(newPercentage);
        }
        else
        {
            LeanTween.value(gameObject, SetPercentage, Percentage, newPercentage, TransitionTime);
            LeanTween.value(gameObject, SetAbsolute, Absolute, absoluteVal, TransitionTime);
        }
    }

    protected void SetPercentage(float percentage)
    {
        Percentage = percentage;

        if (PercentText != null)
        {
            float percRound = Mathf.Round(percentage * 100) / 100;
            PercentText.text = percRound + "%";
        }

        if (Bar != null)
        {
            float grow = percentage * DefaultGrow;
            if (IsVertical)
                Bar.sizeDelta = new Vector2(Bar.sizeDelta.x, grow);
            else
                Bar.sizeDelta = new Vector2(grow, Bar.sizeDelta.y);
        }
    }

    protected void SetAbsolute(float absoluteVal)
    {
        Absolute = absoluteVal;

        if (AbsoluteText != null)
            AbsoluteText.text = Mathf.Round(absoluteVal) + ""; //Temporal
    }

}
