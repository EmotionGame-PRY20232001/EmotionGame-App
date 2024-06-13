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
    protected Dictionary<Emotion.EEmotion, BarStat> EmotionBars { get; set; }
    [field:SerializeField]
    public float Total { get; protected set; }
    [field: SerializeField]
    public float Maximum { get; protected set; }
    //public string Title;

    protected virtual void Awake()
    {
        //Debug.Log("BarChart.Awake " + name);
        EmotionBars = new Dictionary<Emotion.EEmotion, BarStat>();
        Bars = GetComponentsInChildren<BarStat>();
        LoadBarsDict();
        LoadRandom();
    }

    //TODO: Check if better read or spawn bars
    protected virtual void LoadBarsDict()
    {
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

        //Not optimized //TODO: Check
        LoadBarsDict();

        if (Bars == null) return;
        foreach (var value in EmotionValues)
        {
            Total += value.Value;
            Maximum = Mathf.Max(Maximum, value.Value);
        }
        foreach (var value in EmotionValues)
        {
            if (EmotionBars.ContainsKey(value.Key))
                EmotionBars[value.Key]?.LoadPercentage(Total, Maximum, value.Value, true);
        }
    }

    public virtual void LoadRandom()
    {
        Total = 100;
        Maximum = 20;

        if (Bars == null) return;
        foreach (var bar in EmotionBars)
        {
            float value = Random.Range(1, Maximum);
            EmotionBars[bar.Key].LoadPercentage(Total, Maximum, value, true);
        }
    }
}
