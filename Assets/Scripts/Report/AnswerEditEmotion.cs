using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerEditEmotion : MonoBehaviour
{
    [field: SerializeField]
    [SerializedDictionary("Emotion", "Toggle")]
    public SerializedDictionary<Emotion.EEmotion, Toggle> EmotionTooglesDict { get; protected set; }
    [SerializeField]
    protected Answer imitationAnswer;
    [SerializeField]
    protected Response response;

    public void LoadEmotionDetected(ReportManager.FullResponse r)
    {
        if (imitationAnswer != null)
            imitationAnswer.Load(r);

        foreach (var etd in EmotionTooglesDict)
        {
            if (etd.Key == r.response.ResponseEmotionId)
            {
                if (etd.Value != null)
                    etd.Value.isOn = true;
            }
        }
    }

    protected Emotion.EEmotion GetEmotionSelected()
    {
        foreach (var etd in EmotionTooglesDict)
        {
            if (etd.Value.isOn)
                return etd.Key;
        }
        return response.ResponseEmotionId;
    }

    public void ConfirmChangedEmotion()
    {
        Emotion.EEmotion changedEmotion = GetEmotionSelected();
        if (changedEmotion == response.ResponseEmotionId)
            return;

        response.ResponseEmotionId = changedEmotion;
        DBManager.Instance.UpdateResponseToDb(response);
    }
}
