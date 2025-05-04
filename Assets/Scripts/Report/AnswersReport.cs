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

    List<Answer> AnswersLoaded = new List<Answer>();

    protected void SpawnAnswersList()
    {
        if (loaded) return;

        var gm = GameManager.Instance;
        if (Manager == null || Manager.Responses == null || gm == null)
            return;

        foreach (Response resp in Manager.Responses)
        {
            Exercise exercise = GetExercise(resp.ExerciseId);
            ExerciseContent.IdStruct idCont = ExerciseContent.IdStruct.FromString(exercise.ContentId);
            Debug.Log("AnswersReport.SpawnAnswersList\t" + exercise.ActivityId + "\t" + idCont.emotion + " - " + resp.ResponseEmotionId);

            Answer instance = null;
            switch (exercise.ActivityId)
            {
                case EmotionExercise.EActivity.Choose:
                    if (ChooseAnswerPrefab != null)
                    {
                        Sprite exercisePhoto = gm.Emotions[idCont.emotion].ExerciseContents.Faces[idCont.order];
                        instance = Instantiate(ChooseAnswerPrefab, AnswersContainer);
                        instance.LoadChoose(exercisePhoto, idCont.emotion, resp.ResponseEmotionId);
                    }
                    break;

                case EmotionExercise.EActivity.Context:
                    if (ContextAnswerPrefab != null)
                    {
                        string exerciseText = gm.Emotions[idCont.emotion].ExerciseContents.Contexts[idCont.order];
                        instance = Instantiate(ContextAnswerPrefab, AnswersContainer);
                        instance.LoadContext(exerciseText, idCont.emotion, resp.ResponseEmotionId);
                    }
                    break;

                case EmotionExercise.EActivity.Imitate:
                    if (ImitateAnswerPrefab != null)
                    {
                        Sprite exercisePhoto = gm.Emotions[idCont.emotion].ExerciseContents.Faces[idCont.order];
                        Sprite playerPhoto = null;
                        instance = Instantiate(ImitateAnswerPrefab, AnswersContainer);
                        instance.LoadImitate(exercisePhoto, playerPhoto, idCont.emotion, resp.ResponseEmotionId);
                    }
                    break;
            }

            if (instance != null)
                AnswersLoaded.Add(instance);
        }

        loaded = true;
    }

    protected virtual void CleanAnswers()
    {
        if (AnswersLoaded.Count > 0)
        {
            foreach (Answer a in AnswersLoaded)
            {
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
        SpawnAnswersList();
        SnapToTop();
    }

    protected override void OnFilerDates(List<DateTime> dates)
    {
        throw new NotImplementedException();
    }

    protected override void OnGameToggleChanged(EmotionExercise.EActivity game)
    {
        throw new NotImplementedException();
    }
}
