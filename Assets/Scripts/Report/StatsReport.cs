using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsReport : Report
{
    [SerializeField]
    protected BarChart AssertsChart;
    [SerializeField]
    protected BarChart AvgTimeSolveChart;
    [SerializeField]
    protected BarChart EmotionsMissUnderstoodChart;

    public void LoadAssertsChart()
    {
        if (AssertsChart == null || Manager == null) return;

        Dictionary<Emotion.EEmotion, float> emotionValues = new Dictionary<Emotion.EEmotion, float>();
        emotionValues[Emotion.EEmotion.Anger] = 0;
        emotionValues[Emotion.EEmotion.Disgust] = 0;
        emotionValues[Emotion.EEmotion.Fear] = 0;
        emotionValues[Emotion.EEmotion.Happy] = 0;
        emotionValues[Emotion.EEmotion.Sad] = 0;
        emotionValues[Emotion.EEmotion.Surprise] = 0;
        foreach (Response resp in Manager.Responses)
        {
            Debug.Log(resp);
            if (resp.IsCorrect)
            {
                Emotion.EEmotion emotion = GetExerciseEmotion(resp.ExerciseId);
                emotionValues[emotion]++;
            }
        }
        AssertsChart.LoadStats(emotionValues);
    }

    protected override void OnFilerDates(List<System.DateTime> dates)
    {
    }

    protected override void OnGameToggleChanged(EmotionExercise.EActivity game)
    {
    }

    //protected void 
}
