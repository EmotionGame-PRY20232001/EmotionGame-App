using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEngine.UI;
using TMPro;

public class ReportManager : MonoBehaviour
{
    /// UI shared
    [SerializeField]
    public TMP_Text CurrentPlayerName;
    [SerializeField]
    protected TMP_Text CurrentTabName;
    [SerializeField]
    protected TMP_Text CurrentGameName;
    [SerializeField]
    protected TMP_Text CurrentDateText;

    /// Currents
    [field: SerializeField]
    public EmotionExercise.EActivity CurrentGame { get; protected set; }
    [field:SerializeField]
    public List<DateTime> SelectedDates { get; protected set; }
    public List<FullResponse> Responses { get; protected set; }
    public List<FullResponse> FilteredResponses { get; protected set; }

    [field: SerializeField]
    protected StatsReport Stats { get; set; }
    [field: SerializeField]
    protected AnswersReport Answers { get; set; }

    protected void Awake()
    {
        CurrentGame = EmotionExercise.EActivity.None;
        SelectedDates = new List<DateTime>();
        Responses = new List<FullResponse>();
        FilteredResponses = new List<FullResponse>();
        //Debug.Log("ReportManager.Awake ");
    }

    protected void Start()
    {
        //Debug.Log("ReportManager.Start ");
        if (LoadPlayer())
        {
            if (Stats != null)
                Stats.Load();
        }
    }

    protected bool LoadPlayer()
    {
        var gm = GameManager.Instance;
        if (gm != null && gm.IsPlayerActive())
        {
            CurrentPlayerName.text = gm.currentPlayer.Name;
            List<Response> OnlyResponses = DBManager.Instance.GetResponsesByPlayerFromDb(gm.currentPlayer);
            //Debug.LogWarning("ReportManager:LoadPlayer" + OnlyResponses);

            foreach (Response resp in OnlyResponses)
            {
                //Debug.Log(resp);
                FullResponse fr = new FullResponse(resp);
                Responses.Add(fr);
            }

            FilteredResponses = Responses;

            if (Responses.Count > 0)
                return true;

            Debug.LogWarning("ReportManger::LoadPlayer: " + gm.currentPlayer.Name + " has no registered answers");
            return false;
        }

        Debug.Log("ReportManger::LoadPlayer: gm is null or player is not active");
        return false;
    }

    protected void LoadCurrentTab()
    {
        if (Stats.isActiveAndEnabled)
        {
            Stats.Load(true);
        }
        else if (Answers.isActiveAndEnabled)
        {
            Answers.Load(true);
        }
    }

    public void SetReportName(string name)
    {
        if (CurrentTabName != null)
            CurrentTabName.text = name;
    }

    // EmotionExercise.EActivity Choose | Context | Imitate | None
    public void SetGameName(string name)
    {
        CurrentGame = (EmotionExercise.EActivity)System.Enum.Parse(typeof(EmotionExercise.EActivity), name);

        if (CurrentGame == EmotionExercise.EActivity.None)
        {
            FilteredResponses = Responses;
        }
        else
        {
            FilteredResponses = Responses.FindAll(r => r.exercise.ActivityId == CurrentGame);
            Debug.Log("R " + Responses.Count + " \tFR " + FilteredResponses.Count);
        }

        if (CurrentGameName != null)
        {
            switch (CurrentGame)
            {
                case EmotionExercise.EActivity.Choose:
                    CurrentGameName.text = "Elige";
                    break;
                case EmotionExercise.EActivity.Context:
                    CurrentGameName.text = "Reacciona";
                    break;
                case EmotionExercise.EActivity.Imitate:
                    CurrentGameName.text = "Imita";
                    break;
                default:
                    CurrentGameName.text = "Todos";
                    break;
            }
        }

        LoadCurrentTab();
    }

    protected void SetSelectedDates(List<DateTime> selectedDates)
    {
        if (selectedDates != null)
            SelectedDates = selectedDates;

        DateTime minDate = DateTime.MaxValue;
        DateTime maxDate = DateTime.MinValue;
        //TODO: Optimize
        foreach (DateTime date in selectedDates)
        {
            if (date < minDate)
                minDate = date;
            if (date > maxDate)
                maxDate = date;
        }

        if (CurrentDateText != null)
            CurrentDateText.text = minDate.ToShortDateString();

        if (minDate != maxDate)
            CurrentDateText.text += (" - " + maxDate.ToShortDateString());
    }

    public class FullResponse
    {
        public Response response { get; protected set; }
        public Exercise exercise { get; protected set; }
        public ExerciseContent.IdStruct idCont { get; protected set; }

        public FullResponse(Response resp)
        {
            response = resp;
            exercise = GetExercise(resp.ExerciseId);
            idCont = ExerciseContent.IdStruct.FromString(exercise.ContentId);
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
    }
}