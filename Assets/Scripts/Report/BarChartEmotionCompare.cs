using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChartEmotionCompare : BarChart
{
    [SerializeField]
    protected EmotionObject EmotionObj;

    //TODO: Check if better read or spawn bars
    protected override void LoadBarsDict()
    {
        base.LoadBarsDict();
        if (Bars == null || EmotionObj == null) return;

        if (EmotionBars.ContainsKey(EmotionObj.CurrEmotion))
        {
            Destroy(EmotionBars[EmotionObj.CurrEmotion].gameObject);
            EmotionBars.Remove(EmotionObj.CurrEmotion);
        }
    }

    /// <param name="EmotionValues"></param>
    public override void LoadStats<T>(Dictionary<Emotion.EEmotion, T> EmotionValues)
    {
        Emotion.EEmotion emotion = EmotionObj?.CurrEmotion ?? Emotion.EEmotion.Neutral;

        if (EmotionValues.ContainsKey(emotion))
            EmotionValues.Remove(emotion);

        base.LoadStats(EmotionValues);
    }
}
