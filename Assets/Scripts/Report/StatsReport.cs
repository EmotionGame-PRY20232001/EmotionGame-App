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

    protected void LoadResponsesChart()
    {
        if (loaded) return;

        //Debug.Log("StatsReport.LoadResponsesChart [--start--]");
        if (SuccessesChart == null ||
            Manager == null || Manager.Responses == null)
            return;

        var emotionCount = CreateEmotionValuesDict<ushort>(0);
        var emotionSuccesses = CreateEmotionValuesDict<ushort>(0);
        var emotionMistakes = CreateEmotionValuesDict<ushort>(0);
        var emotionAvgSeconds = CreateEmotionValuesDict<float>(0);
        var emotionsConfused = CreateEmotionVsEmotionsDict<ushort>(0);

        foreach (Response resp in Manager.Responses)
        {
            Exercise exercise = GetExercise(resp.ExerciseId);
            Emotion.EEmotion emotion = ExerciseContent.IdStruct.FromString(exercise.ContentId).emotion;
            Debug.Log(resp + "\t[" + exercise.ContentId + "]-"+ exercise.ActivityId);

            if (resp.IsCorrect)
                emotionSuccesses[emotion]++;
            else
                emotionMistakes[emotion]++;

            emotionCount[emotion]++;
            emotionAvgSeconds[emotion] += resp.SecondsToSolve;
            emotionsConfused[emotion][resp.ResponseEmotionId]++;
        }
        // Calc average
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
        //Debug.Log("StatsReport.LoadResponsesChart [--end--]");

        loaded = true;
    }

    protected Dictionary<Emotion.EEmotion, T> CreateEmotionValuesDict<T>(T defaultValue)
    {
        Dictionary<Emotion.EEmotion, T> emotionValues = new Dictionary<Emotion.EEmotion, T>();
        foreach (Emotion.EEmotion emo in Emotion.GetEEmotionsArray())
        {
            if (emo == Emotion.EEmotion.Neutral) continue;
            emotionValues[emo] = defaultValue;
        }
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

    protected override void OnLoad()
    {
        LoadResponsesChart();
    }

    protected override void OnFilerDates(List<System.DateTime> dates)
    {
    }

    protected override void OnGameToggleChanged(EmotionExercise.EActivity game)
    {
    }

    //protected void 
}
