using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EmotionToggles : MonoBehaviour
{
    // [field:SerializeField]
    // public Emotion.EFEmotions Emotions { get; protected set; }

    [field:SerializeField][SerializedDictionary("Emotion", "Toggle")]
    public SerializedDictionary<Emotion.EEmotion, Toggle> Emotions { get; protected set; }

    void Awake()
    {
    }

    public void GetEmotionsChecked()
    {
        // Emotion.EFEmotions emotions;
        List<Emotion.EEmotion> emotions = new List<Emotion.EEmotion>();
        
        foreach (KeyValuePair<Emotion.EEmotion, Toggle> etg in Emotions)
        {
            if (etg.Value != null && etg.Value.isOn)
            {
                emotions.Add(etg.Key);
                Debug.Log(etg.Key);
            }
        }
    }
}
