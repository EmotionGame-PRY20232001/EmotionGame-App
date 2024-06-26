using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Report : MonoBehaviour
{
    [SerializeField]
    protected ReportManager Manager;
    [SerializeField]
    protected string Name;
    [SerializeField]
    protected Button ButtonAux;
    //Filter
    //action button (download|help)


    protected virtual void Start()
    {
        if (Manager != null)
            Manager.SetReportName(Name);
    }

    protected virtual void OnEnable()
    {
        if (ButtonAux != null)
            ButtonAux.gameObject.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        if (ButtonAux != null)
            ButtonAux.gameObject.SetActive(false);
    }

    protected abstract void OnFilerDates(List<System.DateTime> dates);

    protected abstract void OnGameToggleChanged(EmotionExercise.EActivity game);

    protected void OnLoad(List<System.DateTime> dates, EmotionExercise.EActivity game)
    {
    }

    protected Emotion.EEmotion GetExerciseEmotion(int exerciseId)
    {
        //TODO: rework or optimize
        var dbm = DBManager.Instance;
        var gm = GameManager.Instance;
        if (dbm == null || gm == null) return Emotion.EEmotion.Neutral;

        Exercise exercise = dbm.GetExerciseFromDB(exerciseId);
        ExerciseContent.IdStruct idCont = ExerciseContent.IdStruct.FromString(exercise.ContentId);
        //if (idCont.type == ExerciseContent.EValueType.FacePhoto)
        //{
        //gm.Emotions[idCont.emotion].ExerciseContents.Faces[idCont.order];
        //}
        return idCont.emotion;
    }

    //protected abstract void OnGameChoose();
    //protected abstract void OnGameContext();
    //protected abstract void OnGameImitate();
    //protected abstract void OnGameAll();
}
