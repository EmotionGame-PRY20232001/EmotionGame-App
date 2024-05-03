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

    [field:SerializeField][SerializedDictionary("Emotion", "Toggle")]
    public SerializedDictionary<Exercise.EEmotion, Toggle> Emotions { get; protected set; }


    public void SaveEmotionsChecked()
    {
        // Emotion.EFEmotions emotions;
        List<Exercise.EEmotion> emotions = new List<Exercise.EEmotion>();
        
        foreach (KeyValuePair<Exercise.EEmotion, Toggle> etg in Emotions)
        {
            if (etg.Value != null && etg.Value.isOn)
            {
                emotions.Add(etg.Key);
                Debug.Log(etg.Key);
            }
        }

        //TODO: Save to GameManager
        var gm = GameManager.Instance;
        gm.SelectedEmotions = emotions;
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
