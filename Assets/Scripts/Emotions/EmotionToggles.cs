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
    protected uint numActive = 0;

    [field: SerializeField]
    protected Button CheckButton;

    [field:SerializeField][SerializedDictionary("Emotion", "Toggle")]
    public SerializedDictionary<Emotion.EEmotion, Toggle> EmotionTooglesDict { get; protected set; }

    void Start()
    {
        foreach (var emotionToggle in EmotionTooglesDict)
        {
            if (emotionToggle.Value != null)
            {
                emotionToggle.Value.onValueChanged.AddListener(delegate {
                    SetNumActive(emotionToggle.Value);
                });
            }
        }
    }

    void OnEnable()
    {
        if(LoadTogglesOnStart)
        {
            var gm = GameManager.Instance;
            if (gm != null && gm.IsPlayerActive())
                LoadEmotionToggles((Emotion.EEmotions)gm.currentPlayer.EmotionsLearned);
        }
    }

    public void LoadEmotionToggles(Emotion.EEmotions emotionsChecked)
    {
        Debug.Log("EmotionToggles: LoadEmotionToggles: " + emotionsChecked);
        if (emotionsChecked == Emotion.EEmotions.Neutral)
        {
            // if loads from default, all checked
            foreach (KeyValuePair<Emotion.EEmotion, Toggle> etg in EmotionTooglesDict)
            {
                if (etg.Value != null)
                    etg.Value.isOn = true;
            }
        }
        else
        {
            if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Anger))
                EmotionTooglesDict[Emotion.EEmotion.Anger].isOn = emotionsChecked.HasFlag(Emotion.EEmotions.Anger);

            if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Disgust))
                EmotionTooglesDict[Emotion.EEmotion.Disgust].isOn = emotionsChecked.HasFlag(Emotion.EEmotions.Disgust);

            if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Fear))
                EmotionTooglesDict[Emotion.EEmotion.Fear].isOn = emotionsChecked.HasFlag(Emotion.EEmotions.Fear);

            if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Happy))
                EmotionTooglesDict[Emotion.EEmotion.Happy].isOn = emotionsChecked.HasFlag(Emotion.EEmotions.Happy);

            if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Sad))
                EmotionTooglesDict[Emotion.EEmotion.Sad].isOn = emotionsChecked.HasFlag(Emotion.EEmotions.Sad);

            if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Surprise))
                EmotionTooglesDict[Emotion.EEmotion.Surprise].isOn = emotionsChecked.HasFlag(Emotion.EEmotions.Surprise);
        }

        numActive = 0;
        foreach (var emotionToggle in EmotionTooglesDict)
        {
            if (emotionToggle.Value != null && emotionToggle.Value.isOn)
                numActive++;
        }
        ValidateInteractable();
    }

    protected void SetNumActive(Toggle toggle)
    {
        if (toggle.isOn)
        {
            if (numActive < EmotionTooglesDict.Count)
                numActive++;
        }
        else
        {
            if (numActive > 0)
                numActive--;
        }
        ValidateInteractable();
    }

    protected void ValidateInteractable()
    {
        if (CheckButton != null)
            CheckButton.interactable = numActive > 1;
    }

    public void SaveEmotionsChecked()
    {
        List<Emotion.EEmotion> emotions = new List<Emotion.EEmotion>();
        foreach (KeyValuePair<Emotion.EEmotion, Toggle> etg in EmotionTooglesDict)
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
            Emotion.EEmotions emotionsChecked = GetEmotionsFlagSelected();
            gm.currentPlayer.EmotionsLearned = emotionsChecked;
            Debug.Log("EmotionToggles: SaveEmotionsChecked: " + emotionsChecked);
            if (numActive > 0)
                DBManager.Instance.UpdatePlayerToDb(gm.currentPlayer);
        }
    }

    public Emotion.EEmotions GetEmotionsFlagSelected()
    {
        Emotion.EEmotions emotionsChecked = Emotion.EEmotions.Neutral;

        if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Anger) &&
            EmotionTooglesDict[Emotion.EEmotion.Anger].isOn)
            emotionsChecked |= Emotion.EEmotions.Anger;

        if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Disgust) &&
            EmotionTooglesDict[Emotion.EEmotion.Disgust].isOn)
            emotionsChecked |= Emotion.EEmotions.Disgust;

        if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Fear) &&
            EmotionTooglesDict[Emotion.EEmotion.Fear].isOn)
            emotionsChecked |= Emotion.EEmotions.Fear;

        if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Happy) &&
            EmotionTooglesDict[Emotion.EEmotion.Happy].isOn)
            emotionsChecked |= Emotion.EEmotions.Happy;

        if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Sad) &&
            EmotionTooglesDict[Emotion.EEmotion.Sad].isOn)
            emotionsChecked |= Emotion.EEmotions.Sad;

        if (EmotionTooglesDict.ContainsKey(Emotion.EEmotion.Surprise) &&
            EmotionTooglesDict[Emotion.EEmotion.Surprise].isOn)
            emotionsChecked |= Emotion.EEmotions.Surprise;

        return emotionsChecked;
    }

    protected void SaveSelectedGame(EmotionExercise.EActivity activity)
    {
        var gm = GameManager.Instance;
        if (gm != null)
            gm.LastPlayedGame = activity;
    }

    public void SaveLearn()     { SaveSelectedGame(EmotionExercise.EActivity.Learn); }
    public void SaveChoose()    { SaveSelectedGame(EmotionExercise.EActivity.Choose); }
    public void SaveContext()   { SaveSelectedGame(EmotionExercise.EActivity.Context); }
    public void SaveImitate()   { SaveSelectedGame(EmotionExercise.EActivity.Imitate); }
}
