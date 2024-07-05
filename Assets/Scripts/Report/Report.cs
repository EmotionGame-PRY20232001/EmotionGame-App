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

    public void SnapToTop()
    {
        //if (target == null || StepperContent == null || StepperScrollRect == null) return;
        //if (StepperContent.sizeDelta.x <= StepperScrollRect.preferredWidth) return;

        ////https://stackoverflow.com/questions/30766020/how-to-scroll-to-a-specific-element-in-scrollrect-with-unity-ui
        //Canvas.ForceUpdateCanvases();

        //StepperContent.anchoredPosition =
        //        (Vector2)StepperScrollRect.transform.InverseTransformPoint(StepperContent.position)
        //        - (Vector2)StepperScrollRect.transform.InverseTransformPoint(target.position);
    }

    public abstract void Load();

    protected abstract void OnFilerDates(List<System.DateTime> dates);

    protected abstract void OnGameToggleChanged(EmotionExercise.EActivity game);

    protected void OnLoad(List<System.DateTime> dates, EmotionExercise.EActivity game)
    {
    }

    protected Exercise GetExercise(int exerciseId)
    {
        //TODO: rework or optimize
        var dbm = DBManager.Instance;
        var gm = GameManager.Instance;
        if (dbm == null || gm == null) return new Exercise();

        Exercise exercise = dbm.GetExerciseFromDB(exerciseId);
        //ExerciseContent.IdStruct idCont = ExerciseContent.IdStruct.FromString(exercise.ContentId);
        //if (idCont.type == ExerciseContent.EValueType.FacePhoto)
        //{
        //gm.Emotions[idCont.emotion].ExerciseContents.Faces[idCont.order];
        //}
        return exercise;
    }

    //protected abstract void OnGameChoose();
    //protected abstract void OnGameContext();
    //protected abstract void OnGameImitate();
    //protected abstract void OnGameAll();
}
