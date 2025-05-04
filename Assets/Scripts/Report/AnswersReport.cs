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
            Debug.Log("AnswersReport.SpawnAnswersList\t" + exercise.ActivityId + "\t" + idCont.emotion);

            switch(exercise.ActivityId)
            {
                case EmotionExercise.EActivity.Choose:
                    if (ChooseAnswerPrefab != null)
                    {
                        Sprite exercisePhoto = gm.Emotions[idCont.emotion].ExerciseContents.Faces[idCont.order];
                        Answer instance = Instantiate(ChooseAnswerPrefab, AnswersContainer);
                        instance.LoadChoose(exercisePhoto, idCont.emotion, resp.ResponseEmotionId);
                    }
                    break;

                case EmotionExercise.EActivity.Context:
                    if (ContextAnswerPrefab != null)
                    {
                        string exerciseText = gm.Emotions[idCont.emotion].ExerciseContents.Contexts[idCont.order];
                        Answer instance = Instantiate(ContextAnswerPrefab, AnswersContainer);
                        instance.LoadContext(exerciseText, idCont.emotion, resp.ResponseEmotionId);
                    }
                    break;

                case EmotionExercise.EActivity.Imitate:
                    if (ImitateAnswerPrefab != null)
                    {
                        Sprite exercisePhoto = gm.Emotions[idCont.emotion].ExerciseContents.Faces[idCont.order];
                        Sprite playerPhoto = null;
                        Answer instance = Instantiate(ImitateAnswerPrefab, AnswersContainer);
                        instance.LoadImitate(exercisePhoto, playerPhoto, idCont.emotion, resp.ResponseEmotionId);
                    }
                    break;
            }
        }

        loaded = true;
    }

    public override void Load()
    {
        SpawnAnswersList();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SpawnAnswersList();
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
