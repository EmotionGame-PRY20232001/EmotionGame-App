using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will only be used for Emotions.
/// </summary>
[DisallowMultipleComponent]
public class BarChart : MonoBehaviour
{
    [SerializeField]
    protected BarStat[] Bars;
    protected Dictionary<Emotion.EEmotion, BarStat> EmotionBars;
    [field:SerializeField]
    public float Total { get; protected set; }
    [field: SerializeField]
    public float Maximum { get; protected set; }
    //public string Title;

    protected virtual void Awake()
    {
        EmotionBars = new Dictionary<Emotion.EEmotion, BarStat>();
    }

    protected virtual void Start()
    {
        LoadBars();
        LoadRandom();
    }

    //TODO: Check if better read or spawn bars
    protected virtual void LoadBars()
    {
        Bars = GetComponentsInChildren<BarStat>();
        if (Bars == null) return;
        foreach (BarStat barStat in Bars)
        {
            if (barStat != null)
                EmotionBars[barStat.CurrEmotion] = barStat;
        }
    }

    public virtual void LoadStats(Dictionary<Emotion.EEmotion, float> EmotionValues)
    {
        Total = 0;
        Maximum = 0;

        if (Bars == null) return;
        foreach (var value in EmotionValues)
        {
            Total += value.Value;
            Maximum = Mathf.Max(value.Value);
        }
        foreach (var value in EmotionValues)
        {
            EmotionBars[value.Key].LoadPercentage(Maximum, value.Value);
        }
    }

    public virtual void LoadRandom()
    {
        Total = 100;
        Maximum = 100;

        foreach (var bar in EmotionBars)
        {
            float value = Random.Range(1, Maximum);
            EmotionBars[bar.Key].LoadPercentage(Maximum, value);
        }
    }
}
