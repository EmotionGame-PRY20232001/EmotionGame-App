//using AYellowpaper.SerializedCollections;
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
        //Debug.Log("BarChart[" + name + "].Awake ");
        EmotionBars = new Dictionary<Emotion.EEmotion, BarStat>();
        Bars = GetComponentsInChildren<BarStat>();
        LoadBarsDict();
    }

    protected virtual void Start()
    {
        //Debug.Log("BarChart[" + name + "].Start " + EmotionBars.Count);
        if (DBManager.Instance == null)
            LoadRandom();
    }

    //TODO: Check if better read or spawn bars
    protected virtual void LoadBarsDict()
    {
        //Debug.Log("BarChart[" + name + "].LoadBarsDict [--start--] " + EmotionBars.Count);
        if (Bars == null) return;
        foreach (BarStat barStat in Bars)
        {
            if (barStat != null)
                EmotionBars[barStat.CurrEmotion] = barStat;
        }
        //Debug.Log("BarChart[" + name + "].LoadBarsDict [--end--] " + EmotionBars.Count);
    }

    public virtual void LoadStats<T>(Dictionary<Emotion.EEmotion, T> EmotionValues) where T : System.IConvertible
    {
        //Debug.Log("BarChart[" + name + "].LoadStats [--start--]");
        Total = 0;
        Maximum = 0;

        if (Bars == null) return;
        foreach (var value in EmotionValues)
        {
            Total += System.Convert.ToSingle(value.Value);
            Maximum = Mathf.Max(Maximum, System.Convert.ToSingle(value.Value));
        }
        foreach (var value in EmotionValues)
        {
            if (EmotionBars.ContainsKey(value.Key))
                EmotionBars[value.Key]?.LoadPercentage(Total, Maximum, System.Convert.ToSingle(value.Value), true);
        }
        //Debug.Log("BarChart[" + name + "].LoadStats [--end--] " + EmotionBars.Count);
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
        //Debug.Log("BarChart[" + name + "].LoadRandom " + EmotionBars.Count);
    }
}
