using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsReport : Report
{
    [SerializeField]
    protected BarChart SuccessesChart;
    [SerializeField]
    protected BarChart MistakesChart;
    [SerializeField]
    protected BarChart AvgTimeSolveChart;
    [SerializeField]
    protected SerializedDictionary<Emotion.EEmotion, BarChart> EmotionsConfusedCharts;

    public void LoadResponsesChart()
    {
        if (SuccessesChart == null || Manager == null) return;

        var emotionCount = CreateEmotionValuesDict<ushort>(0);
        var emotionSuccesses = CreateEmotionValuesDict<ushort>(0);
        var emotionMistakes = CreateEmotionValuesDict<ushort>(0);
        var emotionAvgSeconds = CreateEmotionValuesDict<float>(0);
        var emotionsConfused = CreateEmotionVsEmotionsDict<ushort>(0);

        foreach (Response resp in Manager.Responses)
        {
            Debug.Log(resp);
            Emotion.EEmotion emotion = GetExerciseEmotion(resp.ExerciseId);

            if (resp.IsCorrect)
                emotionSuccesses[emotion]++;
            else
                emotionMistakes[emotion]++;

            emotionCount[emotion]++;
            emotionAvgSeconds[emotion] += resp.SecondsToSolve;
            emotionsConfused[emotion][resp.ResponseEmotionId]++;
        }
        // Calc average
        //for (ushort i = 0; i < emotionAvgSeconds.Keys.Count; i++)
        //KeyValuePair<Emotion.EEmotion, float> emo in emotionAvgSeconds
        foreach (Emotion.EEmotion emo in Emotion.GetEEmotionsArray())
        {
            if (emotionAvgSeconds.ContainsKey(emo) && emotionCount[emo] > 0)
                emotionAvgSeconds[emo] /= emotionCount[emo];
        }

        SuccessesChart?.LoadStats(emotionSuccesses);
        MistakesChart?.LoadStats(emotionMistakes);
        AvgTimeSolveChart?.LoadStats(emotionAvgSeconds);
        // Load every emotion confused per another
        foreach(var emoChart in EmotionsConfusedCharts)
        {
            if (emoChart.Value == null) continue;
            emoChart.Value.LoadStats(emotionsConfused[emoChart.Key]);
        }
    }

    protected Dictionary<Emotion.EEmotion, T> CreateEmotionValuesDict<T>(T defaultValue)
    {
        Dictionary<Emotion.EEmotion, T> emotionValues = new Dictionary<Emotion.EEmotion, T>();
        foreach (Emotion.EEmotion emo in Emotion.GetEEmotionsArray())
        {
            if (emo == Emotion.EEmotion.Neutral) continue;
            emotionValues[emo] = defaultValue;
        }
        //emotionValues[Emotion.EEmotion.Anger] = 0;
        //emotionValues[Emotion.EEmotion.Disgust] = 0;
        //emotionValues[Emotion.EEmotion.Fear] = 0;
        //emotionValues[Emotion.EEmotion.Happy] = 0;
        //emotionValues[Emotion.EEmotion.Sad] = 0;
        //emotionValues[Emotion.EEmotion.Surprise] = 0;
        return emotionValues;
    }

    protected Dictionary<Emotion.EEmotion, Dictionary<Emotion.EEmotion, T>> CreateEmotionVsEmotionsDict<T>(T defaultValue)
    {
        var dict = new Dictionary<Emotion.EEmotion, Dictionary<Emotion.EEmotion, T>>();
        Emotion.EEmotion[] emotions = Emotion.GetEEmotionsArray();
        foreach (Emotion.EEmotion emo in emotions)
        {
            if (emo == Emotion.EEmotion.Neutral) continue;
            dict[emo] = CreateEmotionValuesDict(defaultValue);
        }
        return dict;
    }

    protected override void OnFilerDates(List<System.DateTime> dates)
    {
    }

    protected override void OnGameToggleChanged(EmotionExercise.EActivity game)
    {
    }

    //protected void 
}
