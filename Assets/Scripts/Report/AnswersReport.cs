using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersReport : Report
{
    [SerializeField]
    Answer ChooseAnswerPrefab;
    [SerializeField]
    Answer ContextAnswerPrefab;
    [SerializeField]
    Answer ImitateAnswerPrefab;
    [SerializeField]
    RectTransform AnswersContainer;
    [SerializeField]
    AnswerFull AnswerFullData;
    [SerializeField]
    PopUp AnswerFullPopUp;

    List<Answer> AnswersLoaded = new List<Answer>();

    protected void SpawnAnswersList()
    {
        if (loaded) return;

        var gm = GameManager.Instance;
        if (Manager == null || Manager.FilteredResponses == null || gm == null)
            return;

        foreach (ReportManager.FullResponse r in Manager.FilteredResponses)
        {
            Debug.Log("AnswersReport.SpawnAnswersList\t" + r.exercise.ActivityId + "\t" + r.idCont.emotion + " - " + r.response.ResponseEmotionId);

            Answer instance = null;
            switch (r.exercise.ActivityId)
            {
                case EmotionExercise.EActivity.Choose:
                    if (ChooseAnswerPrefab != null)
                        instance = Instantiate(ChooseAnswerPrefab, AnswersContainer);
                    break;

                case EmotionExercise.EActivity.Context:
                    if (ContextAnswerPrefab != null)
                        instance = Instantiate(ContextAnswerPrefab, AnswersContainer);
                    break;

                case EmotionExercise.EActivity.Imitate:
                    if (ImitateAnswerPrefab != null)
                        instance = Instantiate(ImitateAnswerPrefab, AnswersContainer);
                    break;
            }

            if (instance != null)
            {
                instance.Load(r);
                instance.OnBtnOpenFull(r, AnswerFullPopUp, AnswerFullData);
                AnswersLoaded.Add(instance);
            }
        }

        loaded = true;
    }

    protected virtual void CleanAnswers()
    {
        if (AnswersLoaded.Count > 0)
        {
            for (int i = AnswersLoaded.Count - 1; i >= 0; i--)
            {
                Answer a = AnswersLoaded[i];
                AnswersLoaded.RemoveAt(i);
                Destroy(a.gameObject);
            }
        }
    }

    public override void SnapToTop()
    {
        if (AnswersLoaded.Count == 0)
        {
            Debug.Log("SnapToTop there are NOT AnswersLoaded");
            return;
        }

        RectTransform rt = (RectTransform)(AnswersLoaded[0].gameObject.transform);
        float answerHeight = rt.sizeDelta.y;

        Vector2 position = AnswersContainer.anchoredPosition;
        position.y = -AnswersLoaded.Count * answerHeight * 0.5f;

        AnswersContainer.anchoredPosition = position;
        //Debug.Log("SnapToTop A:" + AnswersLoaded.Count + "\th:" + answerHeight + "\t" + position.y);
    }

    protected override void OnLoad()
    {
        CleanAnswers();
        SpawnAnswersList();
        SnapToTop();
    }
}
