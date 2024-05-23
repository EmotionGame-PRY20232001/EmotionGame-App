using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class EmotionToggles : MonoBehaviour
{
    // [field:SerializeField]
    // public Emotion.EFEmotions Emotions { get; protected set; }
    public bool LoadTogglesOnStart = false;

    [field:SerializeField][SerializedDictionary("Emotion", "Toggle")]
    public SerializedDictionary<Exercise.EEmotion, Toggle> EmotionTooglesDict { get; protected set; }

    void Start()
    {
        if(LoadTogglesOnStart)
        {
            var gm = GameManager.Instance;
            LoadEmotionToggles((Exercise.EEmotions)gm.currentPlayer.EmotionsLearned);
        }
    }

    public void LoadEmotionToggles(Exercise.EEmotions emotionsChecked)
    {
        Debug.Log("EmotionToggles: LoadEmotionToggles: " + emotionsChecked);
        if (emotionsChecked == Exercise.EEmotions.Neutral)
        {
            // if loads from default, all checked
            foreach (KeyValuePair<Exercise.EEmotion, Toggle> etg in EmotionTooglesDict)
            {
                if (etg.Value != null)
                    etg.Value.isOn = true;
            }
        }
        else
        {
            if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Anger))
                EmotionTooglesDict[Exercise.EEmotion.Anger].isOn = emotionsChecked.HasFlag(Exercise.EEmotions.Anger);

            if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Disgust))
                EmotionTooglesDict[Exercise.EEmotion.Disgust].isOn = emotionsChecked.HasFlag(Exercise.EEmotions.Disgust);

            if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Fear))
                EmotionTooglesDict[Exercise.EEmotion.Fear].isOn = emotionsChecked.HasFlag(Exercise.EEmotions.Fear);

            if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Happy))
                EmotionTooglesDict[Exercise.EEmotion.Happy].isOn = emotionsChecked.HasFlag(Exercise.EEmotions.Happy);

            if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Sad))
                EmotionTooglesDict[Exercise.EEmotion.Sad].isOn = emotionsChecked.HasFlag(Exercise.EEmotions.Sad);

            if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Surprise))
                EmotionTooglesDict[Exercise.EEmotion.Surprise].isOn = emotionsChecked.HasFlag(Exercise.EEmotions.Surprise);
        }
    }

    public void SaveEmotionsChecked()
    {
        List<Exercise.EEmotion> emotions = new List<Exercise.EEmotion>();
        foreach (KeyValuePair<Exercise.EEmotion, Toggle> etg in EmotionTooglesDict)
        {
            if (etg.Value != null && etg.Value.isOn)
            {
                emotions.Add(etg.Key);
                Debug.Log("EmotionToggles: SaveEmotionsChecked: " + etg.Key);
            }
        }

        //TODO: Save to GameManager
        var gm = GameManager.Instance;
        gm.SelectedEmotions = emotions;
        
        if(gm.IsPlayerActive())
        {
            Exercise.EEmotions emotionsChecked = GetEmotionsFlagSelected();
            gm.currentPlayer.EmotionsLearned = (int)emotionsChecked;
            Debug.Log("EmotionToggles: SaveEmotionsChecked: " + emotionsChecked);
            DBManager.Instance.UpdatePlayerToDb(gm.currentPlayer);
        }
    }

    public Exercise.EEmotions GetEmotionsFlagSelected()
    {
        Exercise.EEmotions emotionsChecked = Exercise.EEmotions.Neutral;

        if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Anger) &&
            EmotionTooglesDict[Exercise.EEmotion.Anger].isOn)
            emotionsChecked |= Exercise.EEmotions.Anger;

        if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Disgust) &&
            EmotionTooglesDict[Exercise.EEmotion.Disgust].isOn)
            emotionsChecked |= Exercise.EEmotions.Disgust;

        if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Fear) &&
            EmotionTooglesDict[Exercise.EEmotion.Fear].isOn)
            emotionsChecked |= Exercise.EEmotions.Fear;

        if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Happy) &&
            EmotionTooglesDict[Exercise.EEmotion.Happy].isOn)
            emotionsChecked |= Exercise.EEmotions.Happy;

        if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Sad) &&
            EmotionTooglesDict[Exercise.EEmotion.Sad].isOn)
            emotionsChecked |= Exercise.EEmotions.Sad;

        if (EmotionTooglesDict.ContainsKey(Exercise.EEmotion.Surprise) &&
            EmotionTooglesDict[Exercise.EEmotion.Surprise].isOn)
            emotionsChecked |= Exercise.EEmotions.Surprise;

        return emotionsChecked;
    }

    protected void SaveSelectedGame(Exercise.EActivity activity)
    {
        var gm = GameManager.Instance;
        if (gm != null)
            gm.LastPlayedGame = activity;
    }

    public void SaveLearn()     { SaveSelectedGame(Exercise.EActivity.Learn); }
    public void SaveChoose()    { SaveSelectedGame(Exercise.EActivity.Choose); }
    public void SaveContext()   { SaveSelectedGame(Exercise.EActivity.Context); }
    public void SaveImitate()   { SaveSelectedGame(Exercise.EActivity.Imitate); }
}
