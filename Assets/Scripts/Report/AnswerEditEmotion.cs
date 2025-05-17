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
    protected Button confirmBtn;
    [SerializeField]
    protected ReportManager reportManager;
    [SerializeField]
    protected Answer previousAnswer;
    [SerializeField]
    protected Answer imitationAnswer;
    [SerializeField]
    protected Response response;
    [SerializeField]
    protected Emotion.EEmotion correctEmotion;

    protected void Awake()
    {
        confirmBtn?.onClick.AddListener(ConfirmChangedEmotion);
    }

    public void LoadEmotionDetected(ReportManager.FullResponse r)
    {
        response = r.response;
        correctEmotion = r.idCont.emotion;

        if (imitationAnswer != null)
            imitationAnswer.Load(r);

        foreach (var etd in EmotionTooglesDict)
        {
            if (etd.Value != null)
                etd.Value.isOn = etd.Key == r.response.ResponseEmotionId;
        }
    }

    protected Emotion.EEmotion GetEmotionSelected()
    {
        foreach (var etd in EmotionTooglesDict)
        {
            if (etd.Value != null)
            {
                if (etd.Value.isOn)
                    return etd.Key;
            }
        }
        return response.ResponseEmotionId;
    }

    //onEmotionChanged
    protected void ConfirmChangedEmotion()
    {
        Emotion.EEmotion changedEmotion = GetEmotionSelected();
        if (changedEmotion == response.ResponseEmotionId)
            return;

        response.ResponseEmotionId = changedEmotion;
        response.IsCorrect = changedEmotion == correctEmotion;
        DBManager.Instance.UpdateResponseToDb(response);

        previousAnswer?.LoadEmotion(correctEmotion, changedEmotion);
        reportManager?.onFilterUpdated.Invoke();
    }
}
